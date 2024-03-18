using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.OrganizationDtos;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CharityDonations.Tests.Endpoints.OrganizationsEndpointsTests;

public class GetAllOrganizationsTests
{
 [Fact]
    public async Task Get_Organizations_Returns_Organizations_Response_List()
     {
        // Arrange
        var mockOrganizationRepository = new Mock<IOrganizationsRepository>();
        
        IEnumerable<Organization> mockOrganizations = new List<Organization> //creates an IEnumerable<Organization>
        {
            new() {
                Id = 1,
                Name = "Test Org 1",
                Mission = "Test org mission",
                Description = "Test org description",
                ImageUrl = "https://placehold.co/100",
                Contact = new Contact{
                    Email = "info@testorg1.org",
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
            },
            new() {
                Id = 2,
                Name = "Test Org 2",
                Mission = "Test org 2 mission",
                Description = "Test org 2 description",
                ImageUrl = "https://placehold.co/100",
                Contact = new Contact{
                    Email = "info@testorg2.org",
                    PhoneNumber = "999701110",
                    Address1 = "test address 1",
                    Address2 = "test address 2"
                },
                BankAccount = new BankAccount{
                    AccountNumber = "1001445049",
                    AccountHolderName = "Test Org",
                    BankName = "National Bank",
                    BranchName = "Zomba"
                }
            }
        };
        mockOrganizationRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockOrganizations);

        // Act
        var response = await OrganizationsEndpoints.GetAllOrganizations(mockOrganizationRepository.Object);
      
        //Assert
        Assert.IsType<Ok<IEnumerable<OrganizationDto>>>(response);

        Assert.NotNull(response.Value);
        Assert.NotEmpty(response.Value);
        Assert.Collection(response.Value, organization1 =>
        {
            Assert.Equal(1, organization1.Id);
            Assert.Equal("Test Org 1", organization1.Name);
            Assert.False(organization1.Contact is null);
        }, organization2 =>
        {
            Assert.Equal(2, organization2.Id);
            Assert.Equal("Test Org 2", organization2.Name);
            Assert.True(organization2.Contact.Address3 is null);
        });    
    }
}
