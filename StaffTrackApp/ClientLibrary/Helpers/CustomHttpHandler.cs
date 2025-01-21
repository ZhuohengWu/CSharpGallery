using BaseLibrary.DTOs;
using ClientLibrary.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Helpers;

public class CustomHttpHandler(GetHttpClient getHttpClient, LocalStorageService localStorageService, IUserAccountService userAccountService) : DelegatingHandler
{
    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        bool loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
        bool registerUrl = request.RequestUri!.AbsoluteUri.Contains("register");
        bool refreshTokenUrl = request.RequestUri!.AbsoluteUri.Contains("refresh-token");

        if (loginUrl || registerUrl || refreshTokenUrl)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        var result = await base.SendAsync(request, cancellationToken);
        if (result.StatusCode == HttpStatusCode.Unauthorized)
        {
            // get local token
            var strToken = await localStorageService.GetToken();
            if (strToken == null) { return result; }

            // check token in header
            string token = "";
            try
            {
                token = request.Headers.Authorization!.Parameter!;
            }
            catch { }

            var deserilizeToken = Serializations.DeserializeJsonString<UserSession>(strToken);
            if (deserilizeToken == null) { return result; }

            if (string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", deserilizeToken.Token);
                return await base.SendAsync(request, cancellationToken);
            }

            // refresh and attach new token to the request
            var newJwtToken = await GetRefreshToken(deserilizeToken.RefreshToken!);
            if (string.IsNullOrEmpty (newJwtToken)) { return result; }
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newJwtToken);
            return await base.SendAsync(request, cancellationToken);
        }


        return result;
    }

    private async Task<string> GetRefreshToken(string t)
    {
        var result = await userAccountService.RefreshTokenAsync(new RefreshToken() { Token = t });
        
        string serilizeToken = Serializations.SerializeObj(new UserSession() 
        { Token = result.Token , RefreshToken = result.RefreshToken });

        await localStorageService.SetToken(serilizeToken);

        return result.Token;
    }
}
