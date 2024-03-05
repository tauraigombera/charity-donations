using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Models;

public class Donation
{
    public int Id {get; set;}
    [Required]
    public required decimal Amount {get; set;}
    [Required]
    public required DateTime Date {get; set;}
    [Required]
    public required string TransactionStatus {get; set;}
    public required Organization Organization { get; set; }

    //public required User Donor { get; set; }
}
