using CharityDonations.Api.Dtos.RequestDtos;

namespace CharityDonations.Api.Repositories;

public interface IUserRepository
{
    Task CreateAsync(RegisterRequestDto registerRequest);
}
