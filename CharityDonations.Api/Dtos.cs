using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Dtos;

public record OrganizationDto(
    int Id,
    string Name,
    string Mission,
    string Description,
    string ImageUrl
);

public record CreateOrganizationDto(
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(150)] string Mission,
    [Required] [StringLength(250)] string Description,
    [Url] [StringLength(100)] string ImageUrl
);

public record UpdateOrganizationDto(
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(150)] string Mission,
    [Required] [StringLength(250)] string Description,
    [Url] [StringLength(100)] string ImageUrl
);