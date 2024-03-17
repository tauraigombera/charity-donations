using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Dtos.ContactDtos;

public record UpdateContactDto(
    [Required] [EmailAddress] [StringLength(50)] string Email,
    [Required] [Phone] [StringLength(20)] string PhoneNumber,
    [Required] [StringLength(150)] string Address1,
    [StringLength(150)] string Address2,
    [StringLength(150)] string Address3
);
