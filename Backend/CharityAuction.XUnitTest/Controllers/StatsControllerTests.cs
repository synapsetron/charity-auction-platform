using CharityAuction.API.Controllers;
using CharityAuction.Application.DTO.Bid;
using CharityAuction.Application.DTO.Statistics;
using CharityAuction.Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CharityAuction.API.Tests.Controllers
{
    public class StatsControllerTests
    {
        private readonly Mock<IStatsService> _mockStatsService = new();
        private readonly Mock<ILogger<StatsController>> _mockLogger = new();
        private readonly StatsController _controller;

        public StatsControllerTests()
        {
            _controller = new StatsController(_mockStatsService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetOverview_ShouldReturn200WithFullAdminData()
        {
            // Arrange
            var expectedStats = new StatsOverviewDTO
            {
                Role = "admin",
                UserCount = 100,
                AuctionCount = 20,
                ActiveAuctionCount = 5,
                EndedAuctionCount = 15,
                TotalBidAmount = 12345.67m,
                AvgBidAmount = 100.5m,
                DonationCount = 42,
                MonthlyBids = new[]
                {
            new MonthlyBidStatDTO { Month = "January", Count = 150 },
            new MonthlyBidStatDTO { Month = "February", Count = 200 }
        },
                TopAuctions = new[]
                {
            new PopularAuctionStatDTO { AuctionId = Guid.NewGuid(), AuctionName = "Top Auction", BidCount = 50 }
        },
                TopUsers = new[]
                {
            new TopUserStatDTO { UserId = Guid.NewGuid().ToString(), UserName = "superuser", TotalBids = 1000 }
        }
            };

            _mockStatsService.Setup(s => s.GetOverviewAsync())
                .ReturnsAsync(expectedStats);

            // Act
            var result = await _controller.GetOverview();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().BeEquivalentTo(expectedStats);
        }



        [Fact]
        public async Task GetOverview_ShouldReturn401_WhenUnauthorizedAccessExceptionThrown()
        {
            // Arrange
            _mockStatsService.Setup(s => s.GetOverviewAsync())
                .ThrowsAsync(new UnauthorizedAccessException("Not allowed"));

            // Act
            var result = await _controller.GetOverview();

            // Assert
            var unauthorized = result as UnauthorizedObjectResult;
            unauthorized.Should().NotBeNull();
            unauthorized!.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
            unauthorized.Value.Should().Be("User is not authorized.");
        }

        [Fact]
        public async Task GetOverview_ShouldReturn500_WhenUnhandledExceptionThrown()
        {
            // Arrange
            _mockStatsService.Setup(s => s.GetOverviewAsync())
                .ThrowsAsync(new Exception("Something went wrong"));

            // Act
            var result = await _controller.GetOverview();

            // Assert
            var internalError = result as ObjectResult;
            internalError.Should().NotBeNull();
            internalError!.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            internalError.Value.Should().Be("An error occurred while retrieving statistics.");
        }
    }
}
