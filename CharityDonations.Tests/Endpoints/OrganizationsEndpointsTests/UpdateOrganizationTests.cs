using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.ContactDtos;
using CharityDonations.Api.Dtos.OrganizationDtos;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CharityDonations.Tests.Endpoints.OrganizationsEndpointsTests;

public class UpdateOrganizationTests
{
    [Fact]
    public async Task Put_Organization_Returns_NoContent_When_Updated()
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

        UpdateOrganizationDto updatedOrganization = new(
            Name : "Updated Test Org",
            Mission : "Test org mission updated",
            Description : "Test org description updated",
            ImageUrl : "https://placehold.co/100",
            Contact : new UpdateContactDto
            (
                Email: "info@test.org",
                PhoneNumber: "0888701110",
                Address1: "Test org address1",
                Address2: "Test org address2",
                Address3: "Test org address3"
            )
            //add bank account properties later
        );    

        var mock = new Mock<IOrganizationsRepository>();

        mock.Setup(repo => repo.GetAsync(It.Is<int>(id => id == 1)))
            .ReturnsAsync(existingOrganization);

        mock.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.Is<Organization>(t => 
            
            t.Name == updatedOrganization.Name &&
            t.Mission == updatedOrganization.Mission &&
            t.Description == updatedOrganization.Description && 
            //update contact details
            t.Contact.Email == updatedOrganization.Contact.Email &&
            t.Contact.PhoneNumber == updatedOrganization.Contact.PhoneNumber &&
            t.Contact.Address1 == updatedOrganization.Contact.Address1 &&
            t.Contact.Address2 == updatedOrganization.Contact.Address2 &&
            t.Contact.Address3 == updatedOrganization.Contact.Address3
            //update bank account details later
            )))
            .Callback<int, Organization>((id, organization) => existingOrganization = organization)
            .Returns(Task.CompletedTask);

        // Act
        var result = await OrganizationsEndpoints.UpdateOrganizationAsync(mock.Object, 1, updatedOrganization);

        // Assert  
        Assert.IsType<Results<NoContent, NotFound>>(result);

        var createdResult = result.Result;

        Assert.NotNull(createdResult);

        Assert.Equal("Updated Test Org", existingOrganization.Name);
        Assert.True(existingOrganization.Mission == updatedOrganization.Mission);
    }
}
