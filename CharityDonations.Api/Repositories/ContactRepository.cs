using CharityDonations.Api.Data;
using CharityDonations.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CharityDonations.Api.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ApiDbContext dbContext;

    public ContactRepository(ApiDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Contact?> GetAsync(int contactId)
    {
         return await dbContext.Contacts.FindAsync(contactId);
    }
    public async Task<Contact?> GetByOrganizationIdAsync(int organizationId)
    {
        return await dbContext.Contacts
            .Where(c => c.OrganizationId == organizationId)
            .FirstOrDefaultAsync();
    }  
    public async Task CreateAsync(Contact contact)
    {
        dbContext.Contacts.Add(contact);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contact updatedContact)
    {
        dbContext.Update(updatedContact);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int contactId)
    {
        await dbContext.Contacts.Where(organization => organization.Id == contactId)
                                .ExecuteDeleteAsync();
    }

}
