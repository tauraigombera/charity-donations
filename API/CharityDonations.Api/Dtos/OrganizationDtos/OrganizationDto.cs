using CharityDonations.Api.BankAccountDtos;
using CharityDonations.Api.Dtos.ContactDtos;

namespace CharityDonations.Api.Dtos.OrganizationDtos;
public record OrganizationDto(
    int Id,
    string Name,
    string Mission,
    string Description,
    string ImageUrl,
    ContactDto Contact,
    BankAccountDto BankAccount
);