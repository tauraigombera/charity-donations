using CharityDonations.Api.Models;

namespace CharityDonations.Api.CoreRepositories;

public interface IContactRepository
{
    Task CreateAsync(Contact contact);
    Task DeleteAsync(int contactId);
    Task<Contact?> GetAsync(int contactId);
    Task<Contact?> GetByOrganizationIdAsync(int organizationId);
    Task UpdateAsync(Contact updatedContact);
}
