using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Dtos.RequestDtos;
public record RegisterRequestDto
(
    [Required] string Username,
    [Required] string Email,
    [Required] string Password
);
