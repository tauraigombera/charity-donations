using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.DonationDtos;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CharityDonations.Tests.Endpoints.DonationEndpointsTests;

public class GetDonationsByOrganizationIdTests
{
    [Fact]
    public async Task ReturnsOkResultWithDonationDtoList_WhenDonationsExist()
    {
        // Arrange
        var mockDonationRepository = new Mock<IDonationRepository>();
        var organizationId = 2;
        var expectedDonations = new List<Donation>
        {
            new() { Id = 1, Amount = 10000, DonationDate = DateTime.Today.AddDays(-5), DonorName = "Donor1", OrganizationId = organizationId },
            new() { Id = 2, Amount = 5000, DonationDate = DateTime.Today.AddDays(-3), DonorName = "Donor2", OrganizationId = organizationId }
        };
        mockDonationRepository.Setup(repo => repo.GetAllByOrganizationAsync(organizationId)).ReturnsAsync(expectedDonations);

        // Act
        var response = await DonationsEndpoints.GetDonationsByOrganizationIdAsync(mockDonationRepository.Object, organizationId);

        // Assert
        response.Should().NotBeNull();
        response.Should().BeOfType<Results<Ok<List<DonationDto>>, NotFound>>();
       
        // Extract the value from the response
        var okResult = response.Result as Ok<List<DonationDto>>;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(StatusCodes.Status200OK); // Verify the status code

        var donationDtos = okResult!.Value;
        donationDtos.Should().NotBeNull();
        donationDtos!.Count.Should().Be(expectedDonations.Count);

        // Assert properties of each DonationDto in the list
        donationDtos![0].Id.Should().Be(expectedDonations[0].Id);
        donationDtos![0].Amount.Should().Be(expectedDonations[0].Amount);
        donationDtos![0].DonationDate.Should().Be(expectedDonations[0].DonationDate);
        donationDtos![0].DonorName.Should().Be(expectedDonations[0].DonorName);

        donationDtos![1].Id.Should().Be(expectedDonations[1].Id);
        donationDtos![1].Amount.Should().Be(expectedDonations[1].Amount);
        donationDtos![1].DonationDate.Should().Be(expectedDonations[1].DonationDate);
        donationDtos![1].DonorName.Should().Be(expectedDonations[1].DonorName);
    }
}