using CharityDonations.Api.Models;

namespace CharityDonations.Api.CoreRepositories;

public interface IOrganizationsRepository
{
    Task CreateAsync(Organization organization);
    Task DeleteAsync(int id, Organization organization);
    Task<Organization?> GetAsync(int id);
    Task<IEnumerable<Organization>> GetAllAsync();
    Task UpdateAsync(int id, Organization updatedOrganization);
}