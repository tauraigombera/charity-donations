using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Entities;
public class Organization
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    [Required]
    [StringLength(150)]
    public required string Mission { get; set; }
    [Required]
    [StringLength(250)]
    public required string Description { get; set; }
    [Url]
    [StringLength(100)]
    public required string ImageUrl { get; set; }
}
