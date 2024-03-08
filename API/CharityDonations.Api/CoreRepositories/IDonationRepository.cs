using CharityDonations.Api.Models;

namespace CharityDonations.Api.CoreRepositories;

public interface IDonationRepository
{
    Task CreateAsync(Donation donation);
    Task<Donation?> GetAsync(int id);
    Task<IEnumerable<Donation>> GetAllAsync();
    Task<IEnumerable<Donation>> GetAllByOrganizationAsync(int organizationId);
    
    //  Task<IEnumerable<Donation>> GetAllByUserAsync(User user);
}
