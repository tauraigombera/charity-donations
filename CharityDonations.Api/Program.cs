using System.Text.Json.Serialization;
using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.CoreRepositories.Repositories;
using CharityDonations.Api.Data;
using CharityDonations.Api.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// app registrations
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

/*---------------------------------*/
//middleware configuration for JWT authentication - JWT access token provided by Auth0

builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddAuthorization();

/*---------------------------------*/
builder.Services.AddScoped<IOrganizationsRepository, OrganizationsRepository>();

/*---------------------------------*/
// Custom JSON serialization options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

/*---------------------------------*/
// Read DB connection string from .NET secret manager
var connectionString = builder.Configuration["ConnectionStrings: CharityOrganizationsContext"];
builder.Services.AddSqlServer<ApiDbContext>(connectionString);
 
var app = builder.Build();

/*---------------------------------*/
//apply migrations to database
await app.Services.InitializeDbAsync();

/*---------------------------------*/
// middleware registrations 
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    //change swagger endpoint to "/" instead of "/swagger"
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Charity Donations");
    c.RoutePrefix = string.Empty;
});

/*---------------------------------*/
//configured middleware for authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

/*---------------------------------*/
// endpoint registrations
app.MapOrganizationsEndpoints();

app.Run();
