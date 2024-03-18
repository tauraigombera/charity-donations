using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.OrganizationDtos;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CharityDonations.Tests.Endpoints.OrganizationsEndpointsTests;

public class GetOrganizationByIdTests
{
    [Fact]
    public async Task Get_Organization_By_Id_Returns_Ok_When_Found()
    {
        // Arrange
        var mockOrganizationRepository = new Mock<IOrganizationsRepository>();
        var organizationId = 1;
        var expectedOrganization =  new Organization {
            Id = organizationId,
            Name = "Test Org 1",
            Mission = "Test org mission",
            Description = "Test org description",
            ImageUrl = "https://placehold.co/100",
            Contact = new Contact{
            Email = "info@test.org",
            PhoneNumber = "888701110",
            Address1 = "test address 1",
            Address2 = "test address 2",
            Address3 = "test address 3"
            },
            BankAccount = new BankAccount{
            AccountNumber = "1001577049",
            AccountHolderName = "Test Org",
            BankName = "National Bank",
            BranchName = "Zomba"
            }
        };
        mockOrganizationRepository.Setup(repo => repo.GetAsync(1)).ReturnsAsync(expectedOrganization); 

        // Act  
        var response = await OrganizationsEndpoints.GetOrganizationByIdAsync(mockOrganizationRepository.Object, 1);

        // Assert
        Assert.IsType<Results<Ok<OrganizationDto>, NotFound>>(response);

        var foundResult = response.Result;

        Assert.NotNull(foundResult);
    }

    [Fact]
    public async Task Get_Organization_By_Id_Returns_NotFound_When_Not_Found()
    {
        // Arrange
        var mockOrganizationRepository = new Mock<IOrganizationsRepository>();
       
        mockOrganizationRepository.Setup(repo => repo.GetAsync(It.Is<int>(id => id == 1))) // There is no organization with id = 1
                                  .ReturnsAsync((Organization?)null); 

        // Act  
        var response = await OrganizationsEndpoints.GetOrganizationByIdAsync(mockOrganizationRepository.Object, 1);

        // Assert
        Assert.IsType<Results<Ok<OrganizationDto>, NotFound>>(response);

        var notFoundResult = (NotFound) response.Result;

        Assert.NotNull(notFoundResult);
    }
}
