using Newtonsoft.Json.Linq;
using RestSharp;

namespace CharityDonations.Api.Services.AuthTokenServices;

public class AuthTokenService : IAuthTokenService
{
    private readonly IConfiguration _config;
    public AuthTokenService (IConfiguration config)
    {
        _config = config;
    }
    public string GetToken()
    {
        var client = new RestClient("https://dev-pt56e4yxyx2z3ooq.us.auth0.com/oauth/token");
        var request = new RestRequest("/", Method.Post);
        request.AddHeader("content-type", "application/json");

        // Auth0 keys
        var clientId = _config["Auth0:ClientId"];
        var clientSecret = _config["Auth0:ClientSecret"];
        var audience = _config["Auth0:Audience"];

        request.AddParameter("application/json", $"{{\"client_id\":\"{clientId}\",\"client_secret\":\"{clientSecret}\",\"audience\":\"{audience}\",\"grant_type\":\"client_credentials\"}}", ParameterType.RequestBody);

        var response = client.Execute(request).Content;

        JObject jsonResponse = JObject.Parse(response);

        string jwtAccessToken = jsonResponse["access_token"].ToString();

        return jwtAccessToken;
    }
}
