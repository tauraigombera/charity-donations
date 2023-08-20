using CharityDonations.Api.Models;

namespace CharityDonations.Api.CoreRepositories;

public interface IOrganizationsRepository
{
    Task CreateAsync(Organization organization);
    Task DeleteAsync(int id);
    Task<Organization?> GetAsync(int id);
    Task<IEnumerable<Organization>> GetAllAsync();
    Task UpdateAsync(Organization updatedOrganization);
}