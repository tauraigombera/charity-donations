using CharityDonations.Api.Dtos;
using CharityDonations.Api.Dtos.ContactDtos;
using CharityDonations.Api.Dtos.OrganizationDtos;

namespace CharityDonations.Api.Models;

public static class EntityExtensions
{
    public static OrganizationDto AsDto(this Organization organization)
    {
        return new OrganizationDto(
            organization.Id,
            organization.Name,
            organization.Mission,
            organization.Description,
            organization.ImageUrl,
            organization.Contact.AsDto()
        );
    }

    public static ContactDto AsDto(this Contact contact)
    {
        return new ContactDto
        (
            contact.Id,
            contact.Email,
            contact.PhoneNumber,
            contact.Address1,
            contact.Address2,
            contact.Address3
        );
    }
}
