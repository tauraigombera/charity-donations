using CharityDonations.Api.Authorization;
using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.CoreRepositories.Repositories;
using CharityDonations.Api.Dtos.DonationDtos;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CharityDonations.Api.Endpoints;

public static class DonationsEndpoints
{
    const string GetDonationEndpointName = "GetDonation";
    const string GetDonationsByOrganizationEndpointName = "GetDonationsByOrganization";
    public static RouteGroupBuilder MapDonationsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/donations").WithParameterValidation();
        
        group.MapGet("/", GetAllDonations);
        group.MapGet("/{id}", GetDonationByIdAsync).WithName(GetDonationEndpointName);
        group.MapGet("/{OrganizationId}/Donations", GetDonationsByOrganizationIdAsync).WithName(GetDonationsByOrganizationEndpointName);
        group.MapPost("/", CreateDonationAsync);

        return group;
    }

    //Get all donations
    public static async Task<Ok<IEnumerable<DonationDto>>> GetAllDonations(IDonationRepository repository)
    {
        var donations = await repository.GetAllAsync();
        var donationDtos = donations.Select(donation => donation.AsDto());
        return TypedResults.Ok(donationDtos);
    }

    //Get donation by id
    public static async Task<Results<Ok<DonationDto>, NotFound>> GetDonationByIdAsync(IDonationRepository repository, int id)
    {
        Donation? donation = await repository.GetAsync(id);
        return donation is not null ? TypedResults.Ok(donation.AsDto()) : TypedResults.NotFound();
    }

    //Get donation by organization id
    public static async Task<Results<Ok<List<DonationDto>>, NotFound>> GetDonationsByOrganizationIdAsync(IDonationRepository repository, int organizationId)
    {
        IEnumerable<Donation> donations = await repository.GetAllByOrganizationAsync(organizationId);
        return donations.Any() ? TypedResults.Ok(donations.Select(donation => donation.AsDto()).ToList()) : TypedResults.NotFound();
    }

    //Create organization
    public static async Task<Created<Donation>> CreateDonationAsync (IDonationRepository repository, int organizationId, CreateDonationDto createDonationDto)
    {
        Donation donation = new()
        {
            OrganizationId = organizationId,
            Amount = createDonationDto.Amount,
            DonorName = createDonationDto.DonorName,
            DonationDate = DateTime.Now
        };

        await repository.CreateAsync(donation);
        return TypedResults.Created($"/donations/{donation.Id}", donation);
    }

}
