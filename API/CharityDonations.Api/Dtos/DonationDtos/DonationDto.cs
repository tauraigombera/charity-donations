using CharityDonations.Api.Data;
using CharityDonations.Api.Dtos.OrganizationDtos;

namespace CharityDonations.Api.Dtos.DonationDtos;

public record DonationDto
(
    int Id,
    decimal Amount,
    TransactionStatus TransactionStatus,
    OrganizationDto Organization
);
  
