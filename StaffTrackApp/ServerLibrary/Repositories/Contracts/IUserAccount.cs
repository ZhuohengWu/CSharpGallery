﻿using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts;

public interface IUserAccount
{
    Task<GeneralResponse> CreateAsync(Register user);
    Task<LoginResponse> SignInAsync(Login user);
    Task<LoginResponse> RefreshTokenAsync(RefreshToken token);

    Task<List<ManageUser>> GetUsersAsync();
    Task<GeneralResponse> UpdateUserAsync(ManageUser user);
    Task<List<SystemRole>> GetRolesAsync();
    Task<GeneralResponse> DeleteUserAsync(int id);

    Task<string> GetUserImageAsync(int id);
    Task<bool> UpdateUserProfileAsync(UserProfile profile);
}
