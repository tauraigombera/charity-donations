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
        group.MapGet("/", (IOrganizationsRepository repository) => 
            repository.GetAll().Select(organization => organization.AsDto()));

        //Get organization by id
        group.MapGet("/{id}", (IOrganizationsRepository repository, int id) => 
        {
            Organization? organization = repository.Get(id);
            return organization is not null ? Results.Ok(organization.AsDto()) : Results.NotFound();
        })
        .WithName(GetOrganizationEndpointName);

        //Post an organization
        group.MapPost("/", (IOrganizationsRepository repository, CreateOrganizationDto organizationDto) =>
        {
            Organization organization = new()
            {
                Name = organizationDto.Name,
                Mission = organizationDto.Mission,
                Description = organizationDto.Description,
                ImageUrl = organizationDto.ImageUrl
            };

            repository.Create(organization);
            return Results.CreatedAtRoute(GetOrganizationEndpointName, new {id = organization.Id}, organization);
        });

        //Update an organization
        group.MapPut("/{id}", (IOrganizationsRepository repository, int id, UpdateOrganizationDto updatedOrganizationDto) =>
        {
            Organization? existingOrganization = repository.Get(id);

            if (existingOrganization is null){
                return Results.NotFound();
            }
        
            existingOrganization.Name = updatedOrganizationDto.Name;
            existingOrganization.Mission = updatedOrganizationDto.Mission;
            existingOrganization.Description = updatedOrganizationDto.Description;
            existingOrganization.ImageUrl = updatedOrganizationDto.ImageUrl;

            repository.Update(existingOrganization);

            return Results.NoContent();
        });

        //Delete an organization
        group.MapDelete("/{id}", (IOrganizationsRepository repository, int id) =>
        {
            Organization? organization = repository.Get(id);

            if (organization is not null)
            {
                repository.Delete(id);
            } 

            return Results.NoContent();
        });

        return group;
    }
}
