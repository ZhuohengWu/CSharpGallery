using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public Task<LoginResponse> SignInAsync(Login user)
    {
        throw new NotImplementedException();
    }

    private async Task<ApplicationUser?> FindUserByEmail(string? email)
    {
        return await dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Email!.ToLower() == email!.ToLower());
    }

    private async Task<T> AddToDatabase<T>(T model)
    {
        var result = dbContext.Add(model!);
        await dbContext.SaveChangesAsync();
        return (T)result.Entity;
    }
}
