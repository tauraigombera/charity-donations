using System.Text.Json.Serialization;
using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.CoreRepositories.Repositories;
using CharityDonations.Api.Data;
using CharityDonations.Api.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure API documentation using Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Retrieve OpenApi configuration from app settings
    var openApiSection = builder.Configuration.GetSection("OpenApi");

    // Define Swagger documentation details
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = openApiSection["Version"] ?? "v1",
        Title = openApiSection["Name"] ?? "Charity Donations API",
        Description = openApiSection["Description"] ?? "A .NET Minimal API project aims to provide a platform for facilitating online donations to charitable organizations in Malawi.",
    });
});

// Configure JWT authentication & authorization with Auth0
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // Set Auth0 domain and audience
    options.Authority = builder.Configuration["Auth0:Domain"];
    options.Audience = builder.Configuration["Auth0:Audience"];
});

// Define authorization policy for specific claims
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("organizations:read-write", p => p.
        RequireAuthenticatedUser().
        RequireClaim("permissions", "organizations:read-write"));
});

// Register the OrganizationsRepository for Dependency Injection
builder.Services.AddScoped<IOrganizationsRepository, OrganizationsRepository>();

// Configure custom JSON serialization options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Read DB connection string from .NET secret manager
var connectionString = builder.Configuration["ConnectionStrings:CharityOrganizationsContext"];
builder.Services.AddSqlServer<ApiDbContext>(connectionString);
 
var app = builder.Build();

// Apply migrations to the database
await app.Services.InitializeDbAsync();

// Register Swagger middleware for API documentation
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    // Configure Swagger endpoint and route
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Charity Donations");
    c.RoutePrefix = string.Empty;
});

// Configure middleware for authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Register endpoints for the Organizations API
app.MapOrganizationsEndpoints();

// Start the application
app.Run();
