using CharityDonations.Api.Dtos.RequestDtos;

namespace CharityDonations.Api.CoreRepositories;

public interface IAuthenticationRepository
{
    Task<string> Register(RegisterRequestDto request);
    Task<string> Login(LoginRequestDto request);
}
