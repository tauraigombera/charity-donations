using System.ComponentModel.DataAnnotations;
using CharityDonations.Api.Dtos.ContactDtos;

namespace CharityDonations.Api.Dtos.OrganizationDtos;
public record UpdateOrganizationDto(
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(150)] string Mission,
    [Required] [StringLength(250)] string Description,
    [Url] [StringLength(100)] string ImageUrl,
    [Required] UpdateContactDto Contact
);
