using System.ComponentModel.DataAnnotations;
namespace CharityDonations.Api.Dtos.ContactDtos;
 
public record CreateContactDto(
    [Required] [EmailAddress] [StringLength(50)] string Email,
    [Required] [StringLength(20)] string PhoneNumber,
    [Required] [StringLength(150)] string Address1,
    [StringLength(150)] string Address2,
    [StringLength(150)] string Address3
);
