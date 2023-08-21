using CharityDonations.Api.Dtos.RequestDtos;

namespace CharityDonations.Api.Repositories;

public interface IRegisterRepository
{
    Task<string> Register(RegisterRequestDto request);
}
