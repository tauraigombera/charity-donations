using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Entities;

public class Contact
{
    public int Id { get; set;}
    [Required]
    [StringLength(50)]
    public required string Email { get; set;}
    [Required]
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
    public Organization? Organization { get; set; }
}
