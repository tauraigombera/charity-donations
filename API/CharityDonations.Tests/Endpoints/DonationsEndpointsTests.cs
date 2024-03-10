using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.DonationDtos;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CharityDonations.Tests.Endpoints;

public class DonationsEndpointsTests
{
    [Fact]
    public async Task Get_Donations_Returns_Donations_Response_List()
    {
        // Arrange
        var mockDonationRepository = new Mock<IDonationRepository>();
        IEnumerable<Donation> mockDonations = new List<Donation> //creates an IEnumerable<Donations>
        {
            new Donation {
                Id = 1,
                Amount = 100000,
                DonationDate = DateTime.UtcNow,
                DonorName = "Taurai Gombera", // Placeholder for donor
                OrganizationId = 2
            },
            new Donation {
                Id = 2,
                Amount = 200000,
                DonationDate = DateTime.UtcNow,
                DonorName = "Taurai Gombera", // Placeholder for donor
                OrganizationId = 2
            }
        };
        mockDonationRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockDonations);

        // Act
        var response = await DonationsEndpoints.GetAllDonations(mockDonationRepository.Object);

        //Assert
        Assert.IsType<Ok<IEnumerable<DonationDto>>>(response);

        Assert.NotNull(response.Value);
        Assert.NotEmpty(response.Value);
        Assert.Collection(response.Value, donation1 =>
        {
            Assert.Equal(1, donation1.Id);
            Assert.Equal(100000, donation1.Amount);
            Assert.Equal(2, donation1.OrganizationId);
        }, donation2 =>
        {
            Assert.Equal(2, donation2.Id);
            Assert.Equal(200000, donation2.Amount);
            Assert.Equal(2, donation2.OrganizationId);
        });
    }
}
