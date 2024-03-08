using System.ComponentModel.DataAnnotations;
using CharityDonations.Api.Data;

namespace CharityDonations.Api.Dtos.DonationDtos;

public record CreateOrganizationDto(
    [Required] decimal Amount,
    [Required] DateTime Date,
    [Required] [StringLength(50)] string DonorName,
    [Required] TransactionStatus TransactionStatus,
    [Required] CreateOrganizationDto BankAccount
);