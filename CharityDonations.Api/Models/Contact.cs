using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Models;

public class Contact
{
    public int Id { get; set;}
    [Required]
    [EmailAddress]
    [StringLength(50)]
    public required string Email { get; set;}
    [Required]
    [Phone]
    [StringLength(20)]
    public required string PhoneNumber { get; set; }
    [Required]
    [StringLength(150)]
    public required string Address1 { get; set; }
    [StringLength(150)]
    public string? Address2 { get; set; }
    [StringLength(150)]
    public string? Address3 { get; set; }
    public int OrganizationId { get; set; }
}
