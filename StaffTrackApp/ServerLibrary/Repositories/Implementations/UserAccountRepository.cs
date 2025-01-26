using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations;

public class UserAccountRepository(IOptions<JwtSection> config, StaffTrackDb dbContext) : IUserAccount
{
    public async Task<GeneralResponse> CreateAsync(Register user)
    {
        if (user is null || user.Email is null) return new GeneralResponse(false, "Register model is empty");

        var checkUser = await FindUserByEmail(user.Email);
        if (checkUser is not null) return new GeneralResponse(false, "User is already registered");

        // Save app user
        var appUser = await AddToDatabase(new ApplicationUser()
        {
            FullName = user.FullName,
            Email = user.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
        });

        // create admin role if none exist
        var isAdminRole = await dbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.ToLower() == Constants.Admin.ToLower());
        if (isAdminRole is null)
        {
            var createAdminRole = await AddToDatabase(new SystemRole()
            {
                Name = Constants.Admin
            });

            await AddToDatabase(new UserRole()
            {
                RoleId = createAdminRole.Id,
                UserId = appUser.Id,
            });

            return new GeneralResponse(true, "Admin Account created");
        }

        // create user role 
        var checkUserRole = await dbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.ToLower() == Constants.User.ToLower());
        if (checkUserRole is null)
        {
            var createUserRole = await AddToDatabase(new SystemRole() { Name = Constants.User });
            await AddToDatabase(new UserRole() { RoleId = createUserRole.Id, UserId = appUser.Id, });
        }
        else
        {
            await AddToDatabase(new UserRole() { RoleId = checkUserRole.Id, UserId = appUser.Id, });
        }

        return new GeneralResponse(true, "User Account created!");
    }

    public async Task<LoginResponse> SignInAsync(Login user)
    {
        if (user is null) return new LoginResponse(false, "Login model is empty");

        var appUser = await FindUserByEmail(user.Email);
        if (appUser is null) return new LoginResponse(false, "User not found");

        // check password
        if (!BCrypt.Net.BCrypt.Verify(user.Password, appUser!.Password))
            return new LoginResponse(false, "Email or Password not valid");

        var getUserRole = await FindUserRole(appUser.Id);
        if (getUserRole is null) { return new LoginResponse(false, "User role not found"); }

        var getSystemRole = await FindRoleName(getUserRole.RoleId);
        if (getSystemRole is null) { return new LoginResponse(false, "System role not found"); }

        string jwtToken = GenerateToken(appUser, getSystemRole!.Name!);
        string refreshToken = GenerateRefreshToken();

        var findUser = await dbContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.UserId == appUser.Id);
        if (findUser is not null)
        {
            findUser!.Token = refreshToken;
            await dbContext.SaveChangesAsync();
        }
        else
        {
            await AddToDatabase(new RefreshTokenInfo()
            {
                Token = refreshToken, UserId = appUser.Id
            });
        }

        return new LoginResponse(true, "Login Successfully!", jwtToken, refreshToken );
    }

    private static string GenerateRefreshToken() 
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    private string GenerateToken(ApplicationUser appUser, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new Claim(ClaimTypes.Name, appUser.FullName!),
            new Claim(ClaimTypes.Email, appUser.Email!),
            new Claim(ClaimTypes.Role, role),
        };

        var token = new JwtSecurityToken(
            issuer: config.Value.Issuer,
            audience: config.Value.Audience,
            claims: userClaims,
            expires: DateTime.Now.AddSeconds(10),
            signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private async Task<UserRole?> FindUserRole(int userId)
    {
        return await dbContext.UserRoles.FirstOrDefaultAsync(_ => _.UserId == userId);
    }
    private async Task<SystemRole?> FindRoleName(int roleId)
    {
        return await dbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Id == roleId);
    }
    private async Task<ApplicationUser?> FindUserByEmail(string? email)
    {
        return await dbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Email!.ToLower() == email!.ToLower());
    }

    private async Task<T> AddToDatabase<T>(T model)
    {
        var result = dbContext.Add(model!);
        await dbContext.SaveChangesAsync();
        return (T)result.Entity;
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
    {
        if (token is null) return new LoginResponse(false, "Refresh token model is empty");

        var findToken = await dbContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.Token!.Equals(token.Token));
        if (findToken is null) return new LoginResponse(false, "Refresh token not found");

        var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Id == findToken.UserId);
        if (user is null) return new LoginResponse(false, "Refresh token not generated due to cannot find user");
    
        var userRole = await FindUserRole(user.Id);
        var roleName = await FindRoleName(userRole!.RoleId);
        string jwtToken = GenerateToken(user, roleName!.Name!);
        string refreshToken = GenerateRefreshToken();

        var updateRefreshToken = await dbContext.RefreshTokenInfos.FirstOrDefaultAsync(_ => _.UserId!.Equals(user.Id));
        if (updateRefreshToken is null) return new LoginResponse(false, "Refresh token not generated due to user not logged in");

        updateRefreshToken.Token = refreshToken;
        await dbContext.SaveChangesAsync();
        return new LoginResponse(true, "Token refreshed successfully", jwtToken, refreshToken);
    }

    public async Task<List<ManageUser>> GetUsersAsync()
    {
        var allUsers = await GetAllApplicationUsers();
        var allUserRoles = await GetAllUserRoles();
        var allRoles = await GetAllSystemRoles();

        if (allUsers.Count == 0 || allRoles.Count == 0) return null;

        var users = new List<ManageUser>();
        foreach (var user in allUsers)
        {
            var userRole = allUserRoles.FirstOrDefault(_ => _.Id == user.Id);
            var roleName = allRoles.FirstOrDefault(_ => _.Id == userRole!.RoleId);

            users.Add(new ManageUser()
            {
                UserId = user.Id,
                Name = user.FullName,
                Email = user.Email,
                Role = roleName!.Name
            });
        }
        return users;
    }

    public async Task<GeneralResponse> UpdateUserAsync(ManageUser user)
    {
        var role = (await GetAllSystemRoles()).FirstOrDefault(_ => _.Name == user.Role);
        var userRole = await dbContext.UserRoles.FirstOrDefaultAsync(_ => _.UserId == user.UserId);
        userRole!.RoleId = role!.Id;
        await dbContext.SaveChangesAsync();
        return new GeneralResponse(true, "User role updated successfully");
    }

    public async Task<List<SystemRole>> GetRolesAsync()
    {
        return await GetAllSystemRoles();
    }

    public async Task<GeneralResponse> DeleteUserAsync(int id)
    {
        var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(_ => _.Id == id);
        dbContext.ApplicationUsers.Remove(user!);
        dbContext.SaveChanges();
        return new GeneralResponse(true, "User deleted successfully");
    }

    private async Task<List<ApplicationUser>> GetAllApplicationUsers()
    {
        return await dbContext.ApplicationUsers
            .AsNoTracking()
            .ToListAsync();
    }
    private async Task<List<UserRole>> GetAllUserRoles()
    {
        return await dbContext.UserRoles
            .AsNoTracking()
            .ToListAsync();
    }
    private async Task<List<SystemRole>> GetAllSystemRoles()
    {
        return await dbContext.SystemRoles
            .AsNoTracking()
            .ToListAsync();
    }
}
