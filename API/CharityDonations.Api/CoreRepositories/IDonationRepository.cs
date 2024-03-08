using CharityDonations.Api.Models;

namespace CharityDonations.Api.CoreRepositories;

public interface IDonationRepository
{
    Task CreateAsync(Donation donation);
}
