namespace CharityDonations.Api.Dtos.ContactDtos;
public record ContactDto(
    int Id,
    string Email,
    string PhoneNumber,
    string Address1,
    string ?Address2,
    string ?Address3
);
