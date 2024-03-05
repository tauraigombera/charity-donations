using System.ComponentModel.DataAnnotations;
using CharityDonations.Api.Data;

namespace CharityDonations.Api.Models;

public class Donation
{
    public int Id {get; set;}
    [Required]
    public required decimal Amount {get; set;}
    [Required]
    public required DateTime Date {get; set;}
    [Required]
    public required TransactionStatus TransactionStatus {get; set;}
    public required Organization Organization { get; set; }
    
    // Placeholder for donor
    public required string DonorName { get; set; }
}

