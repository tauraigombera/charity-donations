using CharityDonations.Api.Dtos.RequestDtos;
using CharityDonations.Api.Models;
using CharityDonations.Api.Services;
using Microsoft.AspNetCore.Identity;

namespace CharityDonations.Api.Repositories.AuthRepositories;

public class LoginRepository : ILoginRepository
{
    private readonly UserManager<User> _userManager;
    private readonly IAuthTokenService _authTokenService;
    public LoginRepository (UserManager<User> userManager, IConfiguration config, IAuthTokenService authTokenService)
    {
        _userManager = userManager;
        _authTokenService = authTokenService;
    }
    public async Task<string> Login(LoginRequestDto loginRequest)
    {
        var user = await _userManager.FindByNameAsync(loginRequest.Username) 
                   ?? await _userManager.FindByEmailAsync(loginRequest.Username);

        if (user is null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
        {
            throw new ArgumentException($"Unable to authenticate user {loginRequest.Username}");
        }

        var jwtAccessToken = _authTokenService.GetToken();

        return jwtAccessToken;
    }
}
