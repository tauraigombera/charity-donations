// using System.ComponentModel.DataAnnotations;

// namespace CharityDonations.Api.Dtos;

// public record OrganizationDto(
//     int Id,
//     string Name,
//     string Mission,
//     string Description,
//     string ImageUrl,
//     ContactDto Contact
// );

// public record CreateOrganizationDto(
//     [Required] [StringLength(50)] string Name,
//     [Required] [StringLength(150)] string Mission,
//     [Required] [StringLength(250)] string Description,
//     [Url] [StringLength(100)] string ImageUrl,
//     ContactDto Contact
// );

// public record UpdateOrganizationDto(
//     [Required] [StringLength(50)] string Name,
//     [Required] [StringLength(150)] string Mission,
//     [Required] [StringLength(250)] string Description,
//     [Url] [StringLength(100)] string ImageUrl
// );

// public record ContactDto(
//     int Id,
//     string Email,
//     string PhoneNumber,
//     string Address1,
//     string ?Address2,
//     string ?Address3
// );

// public record CreateContactDto(
//     [Required] [StringLength(50)] string Email,
//     [Required] [StringLength(20)] string PhoneNumber,
//     [Required] [StringLength(150)] string Address1,
//     [Required] [StringLength(150)] string Address2,
//     [Required] [StringLength(150)] string Address3
// );

// public record UpdateContactDto(
//    [Required] [StringLength(50)] string Email,
//     [Required] [StringLength(20)] string PhoneNumber,
//     [Required] [StringLength(150)] string Address1,
//     [Required] [StringLength(150)] string Address2,
//     [Required] [StringLength(150)] string Address3
// );