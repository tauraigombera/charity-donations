using CharityDonations.Api.Models;
using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.OrganizationDtos;
using Microsoft.AspNetCore.Http.HttpResults;
using CharityDonations.Api.Authorization;

namespace CharityDonations.Api.Endpoints;
public static class OrganizationsEndpoints
{
    const string GetOrganizationEndpointName = "GetOrganization";
    public static RouteGroupBuilder MapOrganizationsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/organizations").WithParameterValidation();
        
        group.MapGet("/", GetAllOrganizations);
        group.MapGet("/{id}", GetOrganizationByIdAsync).WithName(GetOrganizationEndpointName).RequireAuthorization(Policies.ReadAccess);
        group.MapPost("/", CreateOrganizationAsync).RequireAuthorization(Policies.WriteAccess);
        group.MapPut("/{id}", UpdateOrganizationAsync).RequireAuthorization(Policies.WriteAccess);
        group.MapDelete("/{id}", DeleteOrganizationAsync).RequireAuthorization(Policies.WriteAccess);

        return group;
    }

    //Get all organizations
    public static async Task<Ok<IEnumerable<OrganizationDto>>> GetAllOrganizations(IOrganizationsRepository repository)
    {
        var organizations = await repository.GetAllAsync();
        var organizationDtos = organizations.Select(organization => organization.AsDto());
        return TypedResults.Ok(organizationDtos);
    }

    //Get organization by id
    public static async Task<Results<Ok<OrganizationDto>, NotFound>> GetOrganizationByIdAsync(IOrganizationsRepository repository, int id)
    {
        Organization? organization = await repository.GetAsync(id);
        return organization is not null ? TypedResults.Ok(organization.AsDto()) : TypedResults.NotFound();
    }

    //Create organization
    public static async Task<Created<Organization>> CreateOrganizationAsync (IOrganizationsRepository repository, CreateOrganizationDto organizationDto)
    {
        Organization organization = new()
        {
            Name = organizationDto.Name,
            Mission = organizationDto.Mission,
            Description = organizationDto.Description,
            ImageUrl = organizationDto.ImageUrl,
            Contact = new Contact
            {
                Email = organizationDto.Contact.Email,
                PhoneNumber = organizationDto.Contact.PhoneNumber,
                Address1 = organizationDto.Contact.Address1,
                Address2 = organizationDto.Contact.Address2,
                Address3 = organizationDto.Contact.Address3
            },
            BankAccount = new BankAccount
            {
                AccountNumber = organizationDto.BankAccount.AccountNumber,
                AccountHolderName = organizationDto.BankAccount.AccountHolderName,
                BankName = organizationDto.BankAccount.BankName,
                BranchName = organizationDto.BankAccount.BranchName,
            }
        };

        await repository.CreateAsync(organization);
        return TypedResults.Created($"/organizations/{organization.Id}", organization);
    }

    //Update organization
    public static async Task<Results<NoContent, NotFound>> UpdateOrganizationAsync (IOrganizationsRepository repository, int id, UpdateOrganizationDto updatedOrganizationDto)
    {
        Organization? existingOrganization = await repository.GetAsync(id);
        
        if (existingOrganization != null){
            existingOrganization.Name = updatedOrganizationDto.Name;
            existingOrganization.Mission = updatedOrganizationDto.Mission;
            existingOrganization.Description = updatedOrganizationDto.Description;
            existingOrganization.ImageUrl = updatedOrganizationDto.ImageUrl;
            
            //Update contact details
            existingOrganization.Contact.Email = updatedOrganizationDto.Contact.Email;
            existingOrganization.Contact.PhoneNumber = updatedOrganizationDto.Contact.PhoneNumber;
            existingOrganization.Contact.Address1 = updatedOrganizationDto.Contact.Address1;
            existingOrganization.Contact.Address2 = updatedOrganizationDto.Contact.Address2;
            existingOrganization.Contact.Address3 = updatedOrganizationDto.Contact.Address3;

            await repository.UpdateAsync(id, existingOrganization);

            return TypedResults.NoContent();
        }
        return TypedResults.NotFound();      
    }

    //Delete organization
    public static async Task<Results<NoContent, NotFound>> DeleteOrganizationAsync(IOrganizationsRepository repository, int id)
    {
        Organization? organization = await repository.GetAsync(id);
        
        if (organization is not null)
        {
            await repository.DeleteAsync(id, organization);
        } 

        return TypedResults.NoContent();
    }
}
