using CharityDonations.Api.Dtos.RequestDtos;
using CharityDonations.Api.Models;

namespace CharityDonations.Api.Repositories;

public interface IUserRepository
{
    User Create(UserRequestDto userRequest);
}
