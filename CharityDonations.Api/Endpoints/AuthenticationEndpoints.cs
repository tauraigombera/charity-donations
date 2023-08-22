using CharityDonations.Api.Repositories;
using CharityDonations.Api.Dtos.RequestDtos;
using Microsoft.AspNetCore.Mvc;

namespace CharityDonations.Api.Endpoints;

public static class AuthenticationEndpoints
{
    public static RouteGroupBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/User")
                .WithParameterValidation();

        group.MapPost("/login", async ([FromBody] LoginRequestDto request, ILoginRepository authService) =>
        {
            var response = await authService.Login(request);
            return Results.Ok(response);
        });

        group.MapPost("/register", async ([FromBody] UserRequestDto userRequest, IRegisterRepository authService) =>
        {
            var response = await authService.Register(userRequest);
            return Results.Ok(response);
        });

        return group;
    } 
}
