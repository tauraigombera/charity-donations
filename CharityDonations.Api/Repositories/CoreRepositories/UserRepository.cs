using CharityDonations.Api.Dtos.RequestDtos;
using CharityDonations.Api.Models;

namespace CharityDonations.Api.Repositories.CoreRepositories;

public class UserRepository : IUserRepository
{
    public User CreateAsync(UserRequestDto userRequest)
    {
        return new User
        {
            Email = userRequest.Email,
            UserName = userRequest.Username,
            SecurityStamp = Guid.NewGuid().ToString()
        };
    }
}
