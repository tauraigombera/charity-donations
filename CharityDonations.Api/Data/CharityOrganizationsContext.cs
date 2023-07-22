using CharityDonations.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CharityDonations.Api.Data;

public class CharityOrganizationsContext : DbContext
{
    public CharityOrganizationsContext(DbContextOptions<CharityOrganizationsContext> options) : base(options)
    {   
    }
    public DbSet<Organization> Organizations { get; set; }
}
