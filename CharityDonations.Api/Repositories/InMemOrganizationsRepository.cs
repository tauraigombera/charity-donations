using CharityDonations.Api.Entities;

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
        },
        new Organization()
        {
            Id = 2,
            Name = "CRECCOM",
            Mission = "Lorem ipsum",
            Description = "Lorem ipsum",
            ImageUrl = "https://placehold.co/100",
        },
        new Organization()
        {
            Id = 3,
            Name = "YONECO",
            Mission = "Lorem ipsum",
            Description = "Lorem ipsum",
            ImageUrl = "https://placehold.co/100",
        }
    };

    public IEnumerable<Organization> GetAll()
    {
        return organizations;
    }

    public Organization? Get(int id)
    {
        return organizations.Find(organization => organization.Id == id);
    }

    public void Create(Organization organization)
    {
        organization.Id = organizations.Max(organization => organization.Id) + 1;
        organizations.Add(organization);
    }

    public void Update(Organization updatedOrganization)
    {
        var index = organizations.FindIndex(organization => organization.Id == updatedOrganization.Id);
        organizations[index] = updatedOrganization;
    }

    public void Delete(int id)
    {
        var index = organizations.FindIndex(organization => organization.Id == id);
        organizations.RemoveAt(index);
    }
}
