using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Dtos.RequestDtos;
public record LoginRequestDto
(
    [Required] string Username,
    [Required] string Password
);

