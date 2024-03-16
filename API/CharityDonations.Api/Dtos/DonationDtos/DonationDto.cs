using CharityDonations.Api.Dtos.OrganizationDtos;

namespace CharityDonations.Api.Dtos.DonationDtos;

public record DonationDto
(
    int Id,
    decimal Amount,
    DateTime DonationDate,
    string DonorName,
    int OrganizationId,
    OrganizationDto? Organization
);
  
