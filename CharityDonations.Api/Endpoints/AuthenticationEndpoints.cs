using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.RequestDtos;
using Microsoft.AspNetCore.Mvc;

namespace CharityDonations.Api.Endpoints;

public static class AuthenticationEndpoints
{
    public static RouteGroupBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/User")
                .WithParameterValidation();

        group.MapPost("/login", async ([FromBody] LoginRequestDto request, IAuthenticationRepository authService) =>
        {
            var response = await authService.Login(request);
            return Results.Ok(response);
        });

        group.MapPost("/register", async ([FromBody] RegisterRequestDto request, IAuthenticationRepository authService) =>
        {
            var response = await authService.Register(request);
            return Results.Ok(response);
        });

        return group;
    } 
}
