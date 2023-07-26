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

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await dbContext.Organizations.AsNoTracking().ToListAsync();
    }

    public async Task<Organization?> GetAsync(int id)
    {
        return await dbContext.Organizations.FindAsync(id);
    }

    public async Task CreateAsync(Organization organization)
    {
        dbContext.Organizations.Add(organization);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Organization updatedOrganization)
    {
        dbContext.Update(updatedOrganization);
        await dbContext.SaveChangesAsync();
    }

     public async Task DeleteAsync(int id)
    {
        await dbContext.Organizations.Where(organization => organization.Id == id)
                               .ExecuteDeleteAsync();
    }
}
