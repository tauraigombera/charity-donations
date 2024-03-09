using CharityDonations.Api.Models;

namespace CharityDonations.Api.CoreRepositories;

public interface IDonationRepository
{
    
    Task<Donation?> GetAsync(int id);
    Task<IEnumerable<Donation>> GetAllAsync();
    Task<IEnumerable<Donation>> GetAllByOrganizationAsync(int organizationId);

    //  Task<IEnumerable<Donation>> GetAllByUserAsync(User user);
    Task CreateAsync(Donation donation);
}
