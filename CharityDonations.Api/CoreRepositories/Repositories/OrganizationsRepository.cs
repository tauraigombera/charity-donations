using CharityDonations.Api.Data;
using CharityDonations.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityDonations.Api.CoreRepositories.Repositories;

public class OrganizationsRepository : IOrganizationsRepository
{
    private readonly ApiDbContext dbContext;
    private readonly ILogger<OrganizationsRepository> logger;

    public OrganizationsRepository(ApiDbContext dbContext, ILogger<OrganizationsRepository> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await dbContext.Organizations
            .Include(x => x.Contact)
            .Include(x => x.BankAccount)
            .AsNoTracking().ToListAsync();
    }

    public async Task<Organization?> GetAsync(int id)
    {
        return await dbContext.Organizations
            .Include(x => x.Contact)
            .Include(x => x.BankAccount)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(Organization organization)
    {
        dbContext.Organizations.Add(organization);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Created an organization: {Name}.", organization.Name);
    }

    public async Task UpdateAsync(int id, Organization updatedOrganization)
    {
        dbContext.Update(updatedOrganization);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Updated an organization with ID: {id}.", id);
    }

    public async Task DeleteAsync(int id, Organization deletedOrganization)
    {
        await dbContext.Organizations.Where(organization => organization.Id == id).ExecuteDeleteAsync();

        logger.LogInformation("Deleted an organization: {Name}.", deletedOrganization.Name);
    }
}
