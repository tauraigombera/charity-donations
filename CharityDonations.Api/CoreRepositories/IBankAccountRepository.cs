using CharityDonations.Api.Models;

namespace CharityDonations.Api;

public interface IBankAccountRepository
{
    Task CreateAsync(BankAccount bankAccount);
    Task DeleteAsync(int bankAccountId);
    Task<BankAccount?> GetAsync(int bankAccountId);
    Task<BankAccount?> GetByOrganizationIdAsync(int organizationId);
    Task UpdateAsync(BankAccount updatedBankAccount);
}
