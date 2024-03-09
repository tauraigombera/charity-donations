using CharityDonations.Api.Authorization;
using CharityDonations.Api.CoreRepositories;
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
        group.MapGet("/{id}", GetDonationByIdAsync).WithName(GetDonationEndpointName).RequireAuthorization(Policies.ReadAccess);
        // group.MapGet("/{OrganizationId}", GetDonationsByOrganizationIdAsync).WithName(GetDonationsByOrganizationEndpointName).RequireAuthorization(Policies.ReadAccess);
        // group.MapPost("/", CreateDonationAsync).RequireAuthorization(Policies.WriteAccess);

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
}
