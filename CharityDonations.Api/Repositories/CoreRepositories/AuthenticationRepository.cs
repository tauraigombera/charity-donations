using CharityDonations.Api.Dtos.RequestDtos;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CharityDonations.Api.Repositories.CoreRepositories;
public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly IConfiguration _config;
    private readonly UserManager<User> _userManager;
    public AuthenticationRepository (UserManager<User> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }
    public async Task<string> Register(RegisterRequestDto request)
    {
        var userByEmail = await _userManager.FindByEmailAsync(request.Email);
        var userByUsername = await _userManager.FindByNameAsync(request.Username);
        if (userByEmail is not null || userByUsername is not null)
        {
            throw new ArgumentException($"User with email {request.Email} or username {request.Username} already exists.");
        }

        User user = new()
        {
            Email = request.Email,
            UserName = request.Username,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new ArgumentException($"Unable to register user {request.Username} errors: {GetErrorsText(result.Errors)}");
        }
        
        return await Login(new LoginRequestDto(request.Email, request.Password));
    }
    public async Task<string> Login(LoginRequestDto request)
    {
        var user = await _userManager.FindByNameAsync(request.Username) 
                   ?? await _userManager.FindByEmailAsync(request.Username);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ArgumentException($"Unable to authenticate user {request.Username}");
        }

        var jwtAccessToken = GetToken();

        return jwtAccessToken;
    }
    private string GetToken()
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
    private static string GetErrorsText(IEnumerable<IdentityError> errors)
    {
        return string.Join(", ", errors.Select(error => error.Description).ToArray());
    }
}
