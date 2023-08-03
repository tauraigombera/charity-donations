using CharityDonations.Api.Models;
using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.OrganizationDtos;

namespace CharityDonations.Api.Endpoints;
public static class OrganizationsEndpoints
{
    const string GetOrganizationEndpointName = "GetOrganization";
    public static RouteGroupBuilder MapOrganizationsEndpoints(this IEndpointRouteBuilder routes)
    {

        var group = routes.MapGroup("/organizations")
                .WithParameterValidation();

        //Get organizations
        group.MapGet("/", async (IOrganizationsRepository repository) =>
        {
            var organizations = await repository.GetAllAsync();
            var organizationDtos = organizations.Select(organization => organization.AsDto());
            return Results.Ok(organizationDtos);
        });


        //Get organization by id
        group.MapGet("/{id}", async (IOrganizationsRepository repository, int id) => 
        {
            Organization? organization = await repository.GetAsync(id);
            return organization is not null ? Results.Ok(organization.AsDto()) : Results.NotFound();
        })
        .WithName(GetOrganizationEndpointName);

        //Post an organization
        group.MapPost("/", async (IOrganizationsRepository repository, CreateOrganizationDto organizationDto) =>
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
            return Results.CreatedAtRoute(GetOrganizationEndpointName, new {id = organization.Id}, organization);
        });

        //Update an organization
        group.MapPut("/{id}", async (IOrganizationsRepository repository, int id, UpdateOrganizationDto updatedOrganizationDto) =>
        {
            Organization? existingOrganization = await repository.GetAsync(id);

            if (existingOrganization is null){
                return Results.NotFound();
            }
        
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

            await repository.UpdateAsync(existingOrganization);

            return Results.NoContent();
        });

        //Delete an organization
        group.MapDelete("/{id}", async (IOrganizationsRepository repository, int id) =>
        {
            Organization? organization = await repository.GetAsync(id);

            if (organization is not null)
            {
                await repository.DeleteAsync(id);
            } 

            return Results.NoContent();
        });

        return group;
    }
}
