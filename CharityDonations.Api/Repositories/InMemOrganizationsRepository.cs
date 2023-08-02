using CharityDonations.Api.Models;

namespace CharityDonations.Api.Repositories;

public class InMemOrganizationsRepository : IOrganizationsRepository
{
    private readonly List<Organization> organizations = new(){
        new Organization()
        {
            Id = 1,
            Name = "OFEST",
            Mission = "Lorem ipsum",
            Description = "Lorem ipsum",
            ImageUrl = "https://placehold.co/100",
            Contact = new Contact {
                Email = "info@ofest.com",
                PhoneNumber = "0999970111",
                Address1 = "Chitimbe",
                Address2 = "",
                Address3 = "",
            }
        },
        new Organization()
        {
            Id = 2,
            Name = "CRECCOM",
            Mission = "Lorem ipsum",
            Description = "Lorem ipsum",
            ImageUrl = "https://placehold.co/100",
             Contact = new Contact {
                Email = "info@crecom.com",
                PhoneNumber = "0999970111",
                Address1 = "Chitimbe",
                Address2 = "",
                Address3 = "",
            }
        },
        new Organization()
        {
            Id = 3,
            Name = "YONECO",
            Mission = "Lorem ipsum",
            Description = "Lorem ipsum",
            ImageUrl = "https://placehold.co/100",
             Contact = new Contact {
                Email = "info@yoneko.org",
                PhoneNumber = "0999970111",
                Address1 = "Chitimbe",
                Address2 = "",
                Address3 = "",
            }
        }
    };

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await Task.FromResult(organizations);
    }

    public async Task<Organization?> GetAsync(int id)
    {
        return await Task.FromResult(organizations.Find(organization => organization.Id == id));
    }

    public async Task CreateAsync(Organization organization)
    {
        organization.Id = organizations.Max(organization => organization.Id) + 1;
        organizations.Add(organization);

        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Organization updatedOrganization)
    {
        var index = organizations.FindIndex(organization => organization.Id == updatedOrganization.Id);
        organizations[index] = updatedOrganization;

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var index = organizations.FindIndex(organization => organization.Id == id);
        organizations.RemoveAt(index);

        await Task.CompletedTask;
    }
}
