using System.Net;
using System.Text.Json;
using NamiClient.Exceptions;
using RestSharp;

namespace NamiClient;

public sealed class NamiRestClient : IDisposable
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

        var result = response.Content == null ? null : JsonSerializer.Deserialize<NamiDataWrapper>(response.Content);

        if (result != null)
        {
            return result.ResponseType switch
            {
                "OK" => result,
                "ERROR" => throw new NamiSessionExpiredException(),
                "EXCEPTION" => throw new NamiAccessViolationException(),
                _ => throw new NamiException(
                    $"Unhandled responseType: {result.ResponseType}. Message: {response.ErrorMessage}")
            };
        }

        return null;
    }

    public void Dispose()
    {
        _restClient.Dispose();
    }
}