using System.ComponentModel.DataAnnotations;
using CharityDonations.Api.Data;
using CharityDonations.Api.Dtos.OrganizationDtos;

namespace CharityDonations.Api.Dtos.DonationDtos;

public record CreateDonationDto(
    [Required] decimal Amount,
    [Required] DateTime Date,
    [Required] [StringLength(50)] string DonorName,
    [Required] TransactionStatus TransactionStatus,
    [Required] int OrganizationId
);