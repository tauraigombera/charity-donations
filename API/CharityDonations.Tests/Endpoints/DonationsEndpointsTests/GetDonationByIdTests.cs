using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.DonationDtos;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CharityDonations.Tests.Endpoints.DonationEndpointsTests;

public class GetDonationByIdTests
{
    [Fact]
    public async Task ReturnsOkResultWithDonationDto_WhenDonationExists()
    {
        // Arrange
        var mockDonationRepository = new Mock<IDonationRepository>();
        var expectedDonation = new Donation
        {
            Id = 1,
            Amount = 100000,
            DonationDate = DateTime.Today,
            DonorName = "Test Donor",
            OrganizationId = 2,
        };
        mockDonationRepository.Setup(repo => repo.GetAsync(1)).ReturnsAsync(expectedDonation);

        // Act
        var response = await DonationsEndpoints.GetDonationByIdAsync(mockDonationRepository.Object, expectedDonation.Id);

        // Assert
        response.Should().BeOfType<Results<Ok<DonationDto>, NotFound>>();

        // Extract the value from the response
        var okResult = response.Result as Ok<DonationDto>;
        var donationDto = okResult?.Value;

        // Assert the properties of the returned DonationDto
        donationDto.Should().NotBeNull();
        donationDto?.Id.Should().Be(expectedDonation.Id);
        donationDto?.Amount.Should().Be(expectedDonation.Amount);
        donationDto?.DonationDate.Should().Be(expectedDonation.DonationDate);
        donationDto?.DonorName.Should().Be(expectedDonation.DonorName);
        donationDto?.OrganizationId.Should().Be(expectedDonation.OrganizationId);
    }

    [Fact]
    public async Task ReturnsNotFoundResult_WhenDonationDoesNotExist()
    {
        // Arrange
        var mockDonationRepository = new Mock<IDonationRepository>();
        mockDonationRepository.Setup(repo => repo.GetAsync(It.IsAny<int>()))
            .ReturnsAsync((Donation?)null);

        // Act
        var response = await DonationsEndpoints.GetDonationByIdAsync(mockDonationRepository.Object, 1);

        // Assert
        response.Should().BeOfType<Results<Ok<DonationDto>, NotFound>>();
    }
}
