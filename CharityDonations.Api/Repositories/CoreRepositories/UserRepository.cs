using CharityDonations.Api.Dtos.RequestDtos;
using CharityDonations.Api.Models;

namespace CharityDonations.Api.Repositories.CoreRepositories;

public class UserRepository : IUserRepository
{
    public User CreateAsync(RegisterRequestDto registerRequest)
    {
        return new User
        {
            Email = registerRequest.Email,
            UserName = registerRequest.Username,
            SecurityStamp = Guid.NewGuid().ToString()
        };
    }
}
