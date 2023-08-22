using CharityDonations.Api.BankAccountDtos;
using CharityDonations.Api.Dtos.RequestDtos;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace CharityDonations.Api.Repositories.AuthRepositories;

public class RegisterRepository : IRegisterRepository
{
    private readonly UserManager<User> _userManager;
    private readonly ILoginRepository _loginRepository;
    public RegisterRepository (UserManager<User> userManager, ILoginRepository loginRepository)
    {
        _userManager = userManager;
        _loginRepository = loginRepository;
    }

    //register user
    public async Task<string> Register(RegisterRequestDto request)
    {
        var userByEmail = await _userManager.FindByEmailAsync(request.Email);
        var userByUsername = await _userManager.FindByNameAsync(request.Username);

        if (userByEmail is not null || userByUsername is not null)
        {
            throw new ArgumentException("User with the provided email or username already exists.");
        }

        var newUser = CreateUser(request);

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            throw new ArgumentException($"Unable to register user {request.Username} errors: {GetErrorsText(result.Errors)}");
        }
        
        return await _loginRepository.Login(new LoginRequestDto(request.Email, request.Password));
    }

    //Create user
    private static User CreateUser(RegisterRequestDto request)
    {
        return new User
        {
            Email = request.Email,
            UserName = request.Username,
            SecurityStamp = Guid.NewGuid().ToString()
        };
    }

    private static string GetErrorsText(IEnumerable<IdentityError> errors)
    {
        return string.Join(", ", errors.Select(error => error.Description).ToArray());
    }
}
