using System.Text.Json.Serialization;
using CharityDonations.Api.Authorization;
using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.CoreRepositories.Repositories;
using CharityDonations.Api.Data;
using CharityDonations.Api.Endpoints;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adding support for API documentation using Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var openApiSection = builder.Configuration.GetSection("OpenApi");
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = openApiSection["Version"] ?? "v1",
        Title = openApiSection["Name"] ?? "Charity Donations API",
        Description = openApiSection["Description"] ?? "A .NET Minimal API project aims to provide a platform for facilitating online donations to charitable organizations in Malawi.",
    });
});

/*---------------------------------*/
//middleware configuration for JWT authentication - JWT access token

builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddAuthorization(options => 
{
    //define read access
    options.AddPolicy(Policies.ReadAccess, builder =>
    builder.RequireClaim("scope", "organisations:read"));

    //define write access
    options.AddPolicy(Policies.WriteAccess, builder =>
    builder.RequireClaim("scope", "organisations:write"));
} );

// Registering the Organizations repository for dependency injection
builder.Services.AddScoped<IOrganizationsRepository, OrganizationsRepository>();

// Configuring JSON serialization options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Configuring database connection
var connectionString = builder.Configuration["ConnectionStrings: CharityOrganizationsContext"];
builder.Services.AddSqlServer<ApiDbContext>(connectionString);
 
var app = builder.Build();

// Initializing the database
await app.Services.InitializeDbAsync();

// Enabling Swagger UI
app.UseSwagger();

// Configuring Swagger UI
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Charity Donations");
    c.RoutePrefix = string.Empty;
});

// Enabling authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Mapping endpoints related to organizations
app.MapOrganizationsEndpoints();

// Running the application
app.Run();
