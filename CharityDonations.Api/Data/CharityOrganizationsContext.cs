using Microsoft.EntityFrameworkCore;
using CharityDonations.Api.Entities;
namespace CharityDonations.Api.Data;

public class CharityOrganizationsContext : DbContext
{
     public CharityOrganizationsContext(DbContextOptions<CharityOrganizationsContext> options) 
         : base(options)
     {   
     }
     public DbSet<Organization> Organizations { get; set; }
     public DbSet<Contact> Contact { get; set; }
}