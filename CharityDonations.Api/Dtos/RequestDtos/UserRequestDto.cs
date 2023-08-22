using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Dtos.RequestDtos;
public record UserRequestDto
(
    [Required] string Username,
    [Required] string Email,
    [Required] string Password
);
