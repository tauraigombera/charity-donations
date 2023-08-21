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
    public async Task<string> Login(LoginRequestDto request)
    {
        var user = await _userManager.FindByNameAsync(request.Username) 
                   ?? await _userManager.FindByEmailAsync(request.Username);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ArgumentException($"Unable to authenticate user {request.Username}");
        }

        var jwtAccessToken = _authTokenService.GetToken();

        return jwtAccessToken;
    }
}
