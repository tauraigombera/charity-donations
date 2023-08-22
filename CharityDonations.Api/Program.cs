using System.Text.Json.Serialization;
using CharityDonations.Api.Repositories;
using CharityDonations.Api.Repositories.CoreRepositories;
using CharityDonations.Api.Data;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using CharityDonations.Api.Repositories.AuthRepositories;
using CharityDonations.Api.Services;
using CharityDonations.Api.Services.AuthTokenServices;

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

// Identity configuration
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

// JWT authentication Auth0
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Auth0:Domain"];
        options.Audience = builder.Configuration["Auth0:Audience"];
    });

builder.Services.AddAuthorization(o =>
    {
        o.AddPolicy("organizations:read-write", p => p.
            RequireAuthenticatedUser().
            RequireClaim("permissions", "organizations:read-write"));
    });

// Dependency Injection registration to IoC system
builder.Services.AddScoped<IOrganizationsRepository, OrganizationsRepository>();
builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IAuthTokenService, AuthTokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Custom JSON serialization options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// DB connection
var connectionString = builder.Configuration["ConnectionStrings: CharityOrganizationsContext"];
builder.Services.AddSqlServer<ApiDbContext>(connectionString);
 
var app = builder.Build();

//apply migrations
await app.Services.InitializeDbAsync();

// middleware registrations 
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    //change swagger endpoint to "/" instead of "/swagger"
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Charity Donations");
    c.RoutePrefix = string.Empty;
});

// authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// endpoint registrations
app.MapAuthenticationEndpoints();
app.MapOrganizationsEndpoints();

app.Run();
