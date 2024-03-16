using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Dtos.DonationDtos;

public record CreateDonationDto(
    [Required] decimal Amount,
    [Required] DateTime Date,
    [Required] [StringLength(50)] string DonorName,
    [Required] int OrganizationId
);