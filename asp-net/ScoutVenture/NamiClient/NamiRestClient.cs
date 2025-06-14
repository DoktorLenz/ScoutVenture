using System.Net;
using System.Text.Json;
using RestSharp;

namespace NamiClient;

public class NamiRestClient : IDisposable
{
    readonly RestClient _restClient;
    
    public NamiRestClient(string memberId, string password)
    {
        var cookieContainer = new CookieContainer();
        var options = new RestClientOptions("https://nami.dpsg.de")
        {
            CookieContainer = cookieContainer,
            FollowRedirects = false,
            Authenticator = new NamiAuthenticator(memberId, password, cookieContainer)
        };
        _restClient = new RestClient(options);
    }

    public async Task<NamiDataWrapper?> GetAllNamiMembersOfGrouping(string groupingId)
    {
        var request = new RestRequest(NamiApiEndpoints.AllMembersOfGrouping(groupingId));
        
        var response = await _restClient.ExecuteAsync(request);
        
        if (!response.IsSuccessful) throw new Exception(response.ErrorMessage);

        return response.Content == null ? null : JsonSerializer.Deserialize<NamiDataWrapper>(response.Content);
    }

    public void Dispose()
    {
        _restClient.Dispose();
    }
}