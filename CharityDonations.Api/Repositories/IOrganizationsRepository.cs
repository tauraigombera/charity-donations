using CharityDonations.Api.Models;

namespace CharityDonations.Api.Repositories;

public interface IOrganizationsRepository
{
    Task CreateAsync(Organization organization);
    Task DeleteAsync(int id);
    Task<Organization?> GetAsync(int id);
    Task<IEnumerable<Organization>> GetAllAsync();
    Task UpdateAsync(Organization updatedOrganization);
}