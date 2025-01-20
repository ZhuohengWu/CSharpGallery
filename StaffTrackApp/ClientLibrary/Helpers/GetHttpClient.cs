using BaseLibrary.DTOs;

namespace ClientLibrary.Helpers;

public class GetHttpClient(IHttpClientFactory httpClientFactory, LocalStorageService localStorageService)
{
    private const string HeaderKey = "Authorization";
    public async Task<HttpClient> GetPrivateHttpClient()
    {
        HttpClient client = httpClientFactory.CreateClient("SystemApiClient");
        var stringToken = await localStorageService.GetToken();
        if (string.IsNullOrEmpty(stringToken) ) return client;

        var deserilizeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
        if (deserilizeToken == null ) return client;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserilizeToken.Token);
        return client;
    }

    public HttpClient GetPublicHttpClient()
    {
        HttpClient client = httpClientFactory.CreateClient("SystemApiClient");
        client.DefaultRequestHeaders.Remove(HeaderKey);
        return client;
    }
}
