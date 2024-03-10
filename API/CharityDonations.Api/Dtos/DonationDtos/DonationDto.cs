using CharityDonations.Api.Data;
using CharityDonations.Api.Dtos.OrganizationDtos;

namespace CharityDonations.Api.Dtos.DonationDtos;

public record DonationDto
(
    int Id,
    decimal Amount,
    DateTime DonationDate,
    string DonorName,
    TransactionStatus? TransactionStatus,
    int OrganizationId,
    OrganizationDto? Organization
);
  
