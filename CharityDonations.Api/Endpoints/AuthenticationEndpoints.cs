using CharityDonations.Api.Repositories;
using CharityDonations.Api.Dtos.RequestDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CharityDonations.Api.Endpoints;

public static class AuthenticationEndpoints
{
    // routes
    public static RouteGroupBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/User")
                          .WithParameterValidation();

        group.MapPost("/login", LoginAsync);

        group.MapPost("/register", RegisterAsync);

        return group;
    }

    // login
    public static async Task<Ok<string>> LoginAsync ([FromBody] LoginRequestDto loginRequest, ILoginRepository loginRepository)
    {
        var response = await loginRepository.Login(loginRequest);
        return TypedResults.Ok(response);
    } 

    // register
    public static async Task<Ok<string>> RegisterAsync ([FromBody] UserRequestDto userRequest, IRegisterRepository registerRepository)
    {
        var response = await registerRepository.Register(userRequest);
        return TypedResults.Ok(response);
    }
}
