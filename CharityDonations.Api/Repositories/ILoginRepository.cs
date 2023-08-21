using CharityDonations.Api.Dtos.RequestDtos;

namespace CharityDonations.Api.Repositories;

public interface ILoginRepository
{
     Task<string> Login(LoginRequestDto request);
}
