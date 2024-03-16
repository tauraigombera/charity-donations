using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Models;

public class Donation
{
    public int Id {get; set;}
    [Required]
    public required decimal Amount {get; set;}
    [Required]
    public required DateTime DonationDate {get; set;}
    [Required]
    public required string DonorName { get; set; }
    [Required]
    public required int OrganizationId { get; set; }
    public Organization? Organization { get; set; }
}

