using System.Net;
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

    public Task<RestResponse> GetAllNamiMembersOfGrouping(string groupingId)
    {
        var request = new RestRequest(NamiApiEndpoints.AllMembersOfGrouping(groupingId));
        return _restClient.ExecuteAsync(request);
    }

    public void Dispose()
    {
        _restClient.Dispose();
    }
}