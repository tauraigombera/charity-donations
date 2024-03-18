using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CharityDonations.Tests.Endpoints.OrganizationsEndpointsTests;

public class DeleteOrganizationTests
{
    [Fact]
    public async Task Delete_Organization_Returns_NoContent_When_Deleted()
    {
        // Arrange
        Organization existingOrganization = new()
        {
            Id = 1,
            Name = "Test Org",
            Mission = "Test org mission",
            Description = "Test org description",
            ImageUrl = "https://placehold.co/100",
            Contact = new Contact
            {
                Id = 1,
                Email = "info@test.org",
                PhoneNumber = "0888701110",
                Address1 = "Test org address1",
                Address2 = "Test org address2",
                Address3 = "Test org address3"
            },
            BankAccount = new BankAccount
            {
                Id = 1,
                AccountNumber = "1001577049",
                AccountHolderName = "Test Org",
                BankName = "National Bank of Malawi",
                BranchName = "City Centre",
            }
        };

        var organizations = new List<Organization> { existingOrganization };

        var mock = new Mock<IOrganizationsRepository>();

        mock.Setup(repo => repo.GetAsync(It.Is<int>(id => id == existingOrganization.Id)))
            .ReturnsAsync(existingOrganization);

        mock.Setup(repo => repo.DeleteAsync(It.Is<int>(id => id == 1), existingOrganization))
            .Callback<int, Organization>((id, organization) => organizations.RemoveAt(0)) //removes the first organization
            .Returns(Task.CompletedTask);

        // Act
        var result = await OrganizationsEndpoints.DeleteOrganizationAsync(mock.Object, existingOrganization.Id);

        // Assert  
        Assert.IsType<Results<NoContent, NotFound>>(result);
        
        var noContentResult = (NoContent) result.Result;

        Assert.NotNull(noContentResult);
        Assert.Empty(organizations);
    }
}
