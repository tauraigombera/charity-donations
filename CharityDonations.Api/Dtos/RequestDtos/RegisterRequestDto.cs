using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Dtos.RequestDtos;
public record RegisterRequestDto
(
    [Required] string UserName,
    [Required] string Email,
    [Required] string Password
);
