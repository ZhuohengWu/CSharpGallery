using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services.Implementations;

public class UserAccountService(GetHttpClient getHttpClient) : IUserAccountService
{
    public const string AuthUrl = "api/authentication";
    public async Task<GeneralResponse> CreateAsync(Register user)
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
        if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error during register");
        return (await result.Content.ReadFromJsonAsync<GeneralResponse>())!;
    }

    public async Task<LoginResponse> SignInAsync(Login user)
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);
        if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error during login");
        return (await result.Content.ReadFromJsonAsync<LoginResponse>())!;
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
    {
        var httpClient = getHttpClient.GetPublicHttpClient();
        var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/refresh-token", token);
        if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error during refresh token");
        return (await result.Content.ReadFromJsonAsync<LoginResponse>())!;
    }

    //public async Task<WeatherForecast[]> GetWeatherForecasts()
    //{
    //    var httpClient = await getHttpClient.GetPrivateHttpClient();
    //    var result = await httpClient.GetFromJsonAsync<WeatherForecast[]>($"api/weatherforecast");
    //    return result!;
    //}

    public async Task<List<ManageUser>> GetUsersAsync()
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var result = await httpClient.GetFromJsonAsync<List<ManageUser>>($"{AuthUrl}/users"); 
        return result!;
    }

    public async Task<GeneralResponse> UpdateUserAsync(ManageUser user)
    {//
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var result = await httpClient.PutAsJsonAsync($"{AuthUrl}/update-user", user);
        if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error during update user");
        return (await result.Content.ReadFromJsonAsync<GeneralResponse>())!;
    }

    public async Task<List<SystemRole>> GetRolesAsync()
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var result = await httpClient.GetFromJsonAsync<List<SystemRole>>($"{AuthUrl}/roles");
        return result!;
    }

    public async Task<GeneralResponse> DeleteUserAsync(int id)
    {
        var httpClient = await getHttpClient.GetPrivateHttpClient();
        var result = await httpClient.DeleteAsync($"{AuthUrl}/delete-user/{id}");
        if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error delete user");
        return (await result.Content.ReadFromJsonAsync<GeneralResponse>())!;
    }
}
