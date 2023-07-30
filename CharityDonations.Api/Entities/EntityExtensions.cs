using System.Reflection.Metadata.Ecma335;
using CharityDonations.Api.Dtos;

namespace CharityDonations.Api.Entities;

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
