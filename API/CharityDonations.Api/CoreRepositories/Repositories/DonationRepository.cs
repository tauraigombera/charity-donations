using CharityDonations.Api.Data;
using CharityDonations.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityDonations.Api.CoreRepositories.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly ApiDbContext dbContext;
    private readonly ILogger<DonationRepository> logger;

    public DonationRepository (ApiDbContext dbContext, ILogger<DonationRepository> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<IEnumerable<Donation>> GetAllAsync()
    {
         return await dbContext.Donations
            .Include(x => x.Organization)
            .AsNoTracking().ToListAsync();
    }

    public async Task<Donation?> GetAsync(int id)
    {
          return await dbContext.Donations
            .Include(x => x.Organization)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Donation>> GetAllByOrganizationAsync(int organizationId)
    {
        return await dbContext.Donations.Where(d => d.Id == organizationId).ToListAsync();
    }

    // public Task<IEnumerable<Donation>> GetAllByUserAsync(User user)
    // {
    //     throw new NotImplementedException();
    // }
    
    public async Task CreateAsync(Donation donation)
    {
        dbContext.Donations.Add(donation);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("A donation has been created by: {Name}.", donation.DonorName);
    }
}
