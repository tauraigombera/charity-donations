using System.Text.Json.Serialization;
using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.CoreRepositories.Repositories;
using CharityDonations.Api.Data;
using CharityDonations.Api.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        Description = openApiSection["Description"] ?? "The Charity Donation API project aims to provide a platform for facilitating online donations to charitable organizations in Malawi. It enables individuals to contribute to various causes and make a positive impact on society. The API will integrate with popular payment gateways to securely handle financial transactions.",
    });
});

// Adding authentication using JWT Bearer token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Domain"];
    options.Audience = builder.Configuration["Auth0:Audience"];
});

// Adding authorization policies
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("organizations:read-write", p => p.
        RequireAuthenticatedUser().
        RequireClaim("permissions", "organizations:read-write"));
});

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
