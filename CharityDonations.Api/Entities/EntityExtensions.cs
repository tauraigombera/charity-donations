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
            organization.ImageUrl
        );
    }
}
