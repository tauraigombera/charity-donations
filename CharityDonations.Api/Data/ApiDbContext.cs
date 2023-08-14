using Microsoft.EntityFrameworkCore;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CharityDonations.Api.Data;

public class ApiDbContext : IdentityDbContext<User>
{
     public ApiDbContext(DbContextOptions<ApiDbContext> options) 
         : base(options)
     {   
     }
     public DbSet<Organization> Organizations { get; set; }
     public DbSet<Contact> Contacts { get; set; }
     public DbSet<BankAccount> BankAccounts { get; set; }
}