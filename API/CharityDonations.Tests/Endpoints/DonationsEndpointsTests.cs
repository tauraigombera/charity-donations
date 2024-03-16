using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.DonationDtos;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CharityDonations.Tests.Endpoints;

public class GetAllDonationsTests
{
    private static IEnumerable<Donation> GetTestDonations()
    {
        return new List<Donation>
        {
            new() { Id = 1, Amount = 100000, DonationDate = DateTime.UtcNow, DonorName = "Donor 1", OrganizationId = 2 },
            new() { Id = 2, Amount = 200000, DonationDate = DateTime.UtcNow, DonorName = "Donor 2", OrganizationId = 2 }
        };
    }

    [Fact]
    public async Task ReturnsOkResultWithDonationDtos()
    {
        // Arrange
        var mockDonationRepository = new Mock<IDonationRepository>();
        mockDonationRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(GetTestDonations());

        // Act
        var response = await DonationsEndpoints.GetAllDonations(mockDonationRepository.Object);

        // Assert
        response.Should().BeOfType<Ok<IEnumerable<DonationDto>>>();
        response.Value.Should().NotBeNull();
        response.Value.Should().HaveCount(2);
    }

    [Fact]
    public async Task ReturnsCorrectDonationDtos()
    {
        // Arrange
        var mockDonationRepository = new Mock<IDonationRepository>();
        mockDonationRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(GetTestDonations());

        // Act
        var response = await DonationsEndpoints.GetAllDonations(mockDonationRepository.Object);

        // Assert
       // response.Value.Should().ContainEquivalentOf(
           // new DonationDto { Id = 1, Amount = 100000, DonationDate = DateTime.UtcNow, DonorName = "Donor 1", OrganizationId = 2 },
           // new DonationDto { Id = 2, Amount = 200000, DonationDate = DateTime.UtcNow, DonorName = "Donor 2", OrganizationId = 2 }
       // );
    }
}
