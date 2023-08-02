using System.ComponentModel.DataAnnotations;
namespace CharityDonations.Api.Dtos.ContactDtos;

public record CreateContactDto(
    [Required] [StringLength(50)] string Email,
    [Required] [StringLength(20)] string PhoneNumber,
    [Required] [StringLength(150)] string Address1,
    [Required] [StringLength(150)] string Address2,
    [Required] [StringLength(150)] string Address3
);
