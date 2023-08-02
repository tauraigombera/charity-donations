using CharityDonations.Api.Data;
using CharityDonations.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityDonations.Api.CoreRepositories.Repositories;

public class OrganizationsRepository : IOrganizationsRepository
{
    private readonly ApiDbContext dbContext;

    public OrganizationsRepository(ApiDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await dbContext.Organizations.Include(x => x.Contact).AsNoTracking().ToListAsync();
    }

    public async Task<Organization?> GetAsync(int id)
    {
        return await dbContext.Organizations.Include(x => x.Contact).SingleOrDefaultAsync(x => x.Id == id);
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
