using Microsoft.EntityFrameworkCore;
using CharityDonations.Api.Entities;
namespace CharityDonations.Api.Data;

public class ApiDbContext : DbContext
{
     public ApiDbContext(DbContextOptions<ApiDbContext> options) 
         : base(options)
     {   
     }
     public DbSet<Organization> Organizations { get; set; }
     public DbSet<Contact> Contacts { get; set; }
}