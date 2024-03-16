using CharityDonations.Api.CoreRepositories;
using CharityDonations.Api.Dtos.DonationDtos;
using CharityDonations.Api.Endpoints;
using CharityDonations.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CharityDonations.Tests.Endpoints.GetAllDonationsTests;

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
            OrganizationId = 2
        };
        mockDonationRepository.Setup(repo => repo.GetAsync(1)).ReturnsAsync(expectedDonation);

        // Act
        var response = await DonationsEndpoints.GetDonationByIdAsync(mockDonationRepository.Object, expectedDonation.Id);

        // Assert
        response.Should().BeOfType<Results<Ok<DonationDto>, NotFound>>();

        if (response is not null)
        {
            var expectedDonationDto = new DonationDto
            (
                expectedDonation.Id,
                expectedDonation.Amount,
                expectedDonation.DonationDate,
                expectedDonation.DonorName,
                expectedDonation.OrganizationId
            );
            response.Result.Should().BeEquivalentTo(expectedDonationDto, options => options.ExcludingMissingMembers());
        }
        else
        {
            response?.Result.Should().BeEquivalentTo(TypedResults.NotFound());
        }
    }
}
