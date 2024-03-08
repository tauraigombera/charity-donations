using Microsoft.EntityFrameworkCore;
using CharityDonations.Api.Models;
namespace CharityDonations.Api.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) 
        : base(options)
    {   
    }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Donation> Donations { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Donation>()
            .Property(d => d.Amount)
            .HasColumnType("decimal(18,2)"); // Adjust precision and scale as per your requirements
    }
}