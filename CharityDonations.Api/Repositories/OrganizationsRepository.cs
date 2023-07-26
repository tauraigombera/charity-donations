using CharityDonations.Api.Data;
using CharityDonations.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CharityDonations.Api.Repositories;

public class OrganizationsRepository : IOrganizationsRepository
{
    private readonly CharityOrganizationsContext dbContext;

    public OrganizationsRepository(CharityOrganizationsContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<Organization> GetAll()
    {
        return dbContext.Organizations.AsNoTracking().ToList();
    }

    public Organization? Get(int id)
    {
        return dbContext.Organizations.Find(id);
    }

    public void Create(Organization organization)
    {
        dbContext.Organizations.Add(organization);
        dbContext.SaveChanges();
    }

    public void Update(Organization updatedOrganization)
    {
        dbContext.Update(updatedOrganization);
        dbContext.SaveChanges();
    }

     public void Delete(int id)
    {
        dbContext.Organizations.Where(organization => organization.Id == id)
                               .ExecuteDelete();
    }
}
