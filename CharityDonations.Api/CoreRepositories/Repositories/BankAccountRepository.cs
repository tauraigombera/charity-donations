using CharityDonations.Api.Data;
using CharityDonations.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityDonations.Api.CoreRepositories.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly ApiDbContext dbContext;

    public BankAccountRepository(ApiDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task CreateAsync(BankAccount bankAccount)
    {
        dbContext.BankAccounts.Add(bankAccount);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int bankAccountId)
    {
        await dbContext.BankAccounts
            .Where(organization => organization.Id == bankAccountId)
            .ExecuteDeleteAsync();
    }

    public async Task<BankAccount?> GetAsync(int bankAccountId)
    {
        return await dbContext.BankAccounts.FindAsync(bankAccountId);
    }

    public async Task<BankAccount?> GetByOrganizationIdAsync(int organizationId)
    {
        return await dbContext.BankAccounts
            .Where(c => c.OrganizationId == organizationId)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(BankAccount updatedBankAccount)
    {
        dbContext.Update(updatedBankAccount);
        await dbContext.SaveChangesAsync();
    }
}

