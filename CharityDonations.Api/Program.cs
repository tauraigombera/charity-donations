using CharityDonations.Api.Data;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Repositories;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddSingleton<IOrganizationsRepository, InMemOrganizationsRepository>();

// Read DB connection string from .NET secret manager
var connectionString = builder.Configuration["ConnectionStrings: CharityOrganizationsContext"];
builder.Services.AddSqlServer<CharityOrganizationsContext>(connectionString);
 
var app = builder.Build();

//apply migrations to database
app.Services.InitializeDb();

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
// endpoint registrations
app.MapOrganizationsEndpoints();

app.Run();
