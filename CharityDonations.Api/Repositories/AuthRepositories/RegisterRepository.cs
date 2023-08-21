using CharityDonations.Api.Dtos.RequestDtos;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace CharityDonations.Api.Repositories.AuthRepositories;

public class RegisterRepository : IRegisterRepository
{
    private readonly UserManager<User> _userManager;
    private readonly ILoginRepository _loginRepository;
    public RegisterRepository (UserManager<User> userManager, IConfiguration config, ILoginRepository loginRepository)
    {
        _userManager = userManager;
        _loginRepository = loginRepository;
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
        
        return await _loginRepository.Login(new LoginRequestDto(request.Email, request.Password));
    }

    private static string GetErrorsText(IEnumerable<IdentityError> errors)
    {
        return string.Join(", ", errors.Select(error => error.Description).ToArray());
    }
}
