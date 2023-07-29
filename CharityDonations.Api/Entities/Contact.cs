using System.Text.Json.Serialization;

namespace CharityDonations.Api.Entities;

public class Contact
{
    public int Id { get; set;}
    public required string Email { get; set;}
    public required string PhoneNumber { get; set; }
    public required string Address1 { get; set; }
    public required string Address2 { get; set; }
    public string? Address3 { get; set; }
    public int OrganizationId { get; set; }
    public required Organization Organization { get; set; }
}
