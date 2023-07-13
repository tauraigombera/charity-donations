using CharityDonations.Api.Entities;

namespace CharityDonations.Api.Repositories;

public interface IOrganizationsRepository
{
    void Create(Organization organization);
    void Delete(int id);
    Organization? Get(int id);
    IEnumerable<Organization> GetAll();
    void Update(Organization updatedOrganization);
}