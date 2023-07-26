using CharityDonations.Api.Dtos;
using CharityDonations.Api.Entities;
using CharityDonations.Api.Repositories;

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
            (await repository.GetAllAsync()).Select(organization => organization.AsDto()));

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
                ImageUrl = organizationDto.ImageUrl
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
