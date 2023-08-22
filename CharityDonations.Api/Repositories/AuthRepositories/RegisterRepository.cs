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
    public async Task<string> Register(UserRequestDto userRequest)
    {
        var userByEmail = await _userManager.FindByEmailAsync(userRequest.Email);
        var userByUsername = await _userManager.FindByNameAsync(userRequest.Username);

        if (userByEmail is not null || userByUsername is not null)
        {
            throw new ArgumentException("User with the provided email or username already exists.");
        }

        var newUser = CreateUser(userRequest);

        var result = await _userManager.CreateAsync(newUser, userRequest.Password);

        if (!result.Succeeded)
        {
            throw new ArgumentException($"Unable to register user {userRequest.Username} errors: {GetErrorsText(result.Errors)}");
        }
        
        return await _loginRepository.Login(new LoginRequestDto(userRequest.Email, userRequest.Password));
    }

    //Create user
    private static User CreateUser(UserRequestDto userRequest)
    {
        return new User
        {
            Email = userRequest.Email,
            UserName = userRequest.Username,
            SecurityStamp = Guid.NewGuid().ToString()
        };
    }

    private static string GetErrorsText(IEnumerable<IdentityError> errors)
    {
        return string.Join(", ", errors.Select(error => error.Description).ToArray());
    }
}
