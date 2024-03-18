using CharityDonations.Api.BankAccountDtos;
using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.ContactDtos;
using CharityDonations.Api.Dtos.OrganizationDtos;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CharityDonations.Tests.Endpoints.OrganizationsEndpointsTests;

public class CreateOrganizationTests
{
    [Fact]
    public async Task Post_Organization_Returns_Created()
    {
        // Arrange
        var organizations = new List<Organization>();

        CreateOrganizationDto newOrganization = new(
            Name: "Test Org",
            Mission: "Test org mission",
            Description: "Test org description",
            ImageUrl: "https://placehold.co/100",
            Contact: new CreateContactDto(
                Email: "info@test.org",
                PhoneNumber: "0999970111",
                Address1: "Test org address1",
                Address2: "Test org address2",
                Address3: "Test org address3"
            ),
            BankAccount: new CreateBankAccountDto(
                AccountNumber: "1001577049",
                AccountHolderName: "Test Org",
                BankName: "National Bank",
                BranchName: "Zomba"
            )
        );

        var mock = new Mock<IOrganizationsRepository>();

        mock.Setup(repo => repo.CreateAsync(It.Is<Organization>(
            t => t.Name == newOrganization.Name && 
            t.Mission == newOrganization.Mission && 
            t.Description == newOrganization.Description && 
            t.ImageUrl == newOrganization.ImageUrl)))
            .Callback<Organization>(organizations.Add)
            .Returns(Task.CompletedTask);

        // Act
        var result = await OrganizationsEndpoints.CreateOrganizationAsync(mock.Object, newOrganization);

        // Assert 
        Assert.IsType<Created<Organization>>(result);

        Assert.NotNull(result);
        Assert.NotNull(result.Location);

        Assert.NotEmpty(organizations);
        Assert.Collection(organizations, organization =>
        {
            Assert.Equal("Test Org", organization.Name);
            Assert.Equal("Test org description", organization.Description);
            Assert.False(organization.Contact is null);
            Assert.False(organization.BankAccount is null);
        }); 
    }
}
