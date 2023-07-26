using Microsoft.EntityFrameworkCore;

namespace CharityDonations.Api.Data;

public static class DataExtensions
{
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CharityOrganizationsContext>();
        dbContext.Database.Migrate();
    }
}
