using System.ComponentModel.DataAnnotations;
using CharityDonations.Api.BankAccountDtos;
using CharityDonations.Api.Dtos.ContactDtos;

namespace CharityDonations.Api.Dtos.OrganizationDtos;

public record CreateOrganizationDto(
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(150)] string Mission,
    [Required] [StringLength(250)] string Description,
    [Url] [StringLength(100)] string ImageUrl,
    [Required] CreateContactDto Contact,
    [Required] CreateBankAccountDto BankAccount
);
