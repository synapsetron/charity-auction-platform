using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Identity;
using CharityAuction.Application.Interfaces;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Application.DTO.Bid;
using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Repositories.Interfaces.Base;
using CharityAuction.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

using MockQueryable;

namespace CharityAuction.XUnitTest.Services.Statistic;

public class StatsServiceTests
{
    private readonly Mock<IRepositoryWrapper> _repositoryMock = new();
    private readonly Mock<ICurrentUserService> _currentUserMock = new();
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<IBidService> _bidServiceMock = new();

    public StatsServiceTests()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            store.Object, null, null, null, null, null, null, null, null
        );
    }

    private StatsService CreateService() =>
        new(_repositoryMock.Object, _currentUserMock.Object, _bidServiceMock.Object, _userManagerMock.Object);

    [Fact(DisplayName = "Throws UnauthorizedAccessException when user not found")]
    public async Task GetOverviewAsync_ShouldThrow_WhenUserNotFound()
    {
        // Arrange
        var userId = "user-404";
        _currentUserMock.Setup(c => c.GetUserId()).Returns(userId);
        _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null!);

        var service = CreateService();

        // Act
        Func<Task> act = async () => await service.GetOverviewAsync();

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("User not found");
    }

    [Fact(DisplayName = "Returns seller stats correctly")]
    public async Task GetOverviewAsync_Seller_ReturnsCorrectStats()
    {
        // Arrange
        var userId = "seller-123";
        var auctionId1 = Guid.NewGuid();
        var auctionId2 = Guid.NewGuid();
        var seller = new ApplicationUser { Id = userId, Role = "Seller", Balance = 123.45m };

        _currentUserMock.Setup(x => x.GetUserId()).Returns(userId);
        _userManagerMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(seller);

        // Fake ended auctions
        var endedAuctions = new List<Auction>
        {
            new() { Id = auctionId1, IsActive = false, EndTime = DateTime.UtcNow.AddDays(-1) },
            new() { Id = auctionId2, IsActive = false, EndTime = DateTime.UtcNow.AddDays(-1) }
        }.AsQueryable().BuildMock();

        var userBids = new List<Bid>
        {
            new() { AuctionId = auctionId1, Amount = 100, UserId = userId },
            new() { AuctionId = auctionId1, Amount = 150, UserId = userId }, // winner
            new() { AuctionId = auctionId2, Amount = 50, UserId = userId }   // winner
        }.AsQueryable().BuildMock();

        var auctionRepo = new Mock<IAuctionRepository>();
        auctionRepo.Setup(r => r.FindAll(It.IsAny<Expression<Func<Auction, bool>>?>())).Returns(endedAuctions);
        var bidRepo = new Mock<IBidRepository>();
        bidRepo.Setup(r => r.FindAll(It.IsAny<Expression<Func<Bid, bool>>?>())).Returns(userBids);

        _repositoryMock.Setup(r => r.AuctionRepository).Returns(auctionRepo.Object);
        _repositoryMock.Setup(r => r.BidRepository).Returns(bidRepo.Object);

        var service = CreateService();

        // Act
        var result = await service.GetOverviewAsync();

        // Assert
        result.Role.Should().Be("Seller");
        result.MyWins.Should().Be(2);
        result.Balance.Should().Be(123.45m);
    }

    [Fact(DisplayName = "Returns admin stats correctly")]
    public async Task GetOverviewAsync_Admin_ReturnsCorrectStats()
    {
        // Arrange
        var admin = new ApplicationUser { Id = "admin", Role = "Admin", FirstName = "John", LastName = "Doe" };

        _currentUserMock.Setup(x => x.GetUserId()).Returns(admin.Id);
        _userManagerMock.Setup(x => x.FindByIdAsync(admin.Id)).ReturnsAsync(admin);
        _userManagerMock.Setup(x => x.Users).Returns(new List<ApplicationUser> { admin }.AsQueryable().BuildMock());

        var auctions = new List<Auction>
        {
            new() { Id = Guid.NewGuid(), IsActive = true },
            new() { Id = Guid.NewGuid(), IsActive = false, EndTime = DateTime.UtcNow.AddDays(-2) }
        }.AsQueryable().BuildMock();

        var auctionRepo = new Mock<IAuctionRepository>();
        auctionRepo.Setup(r => r.FindAll(null)).Returns(auctions);
        auctionRepo.Setup(r => r.FindAll(It.IsAny<Expression<Func<Auction, bool>>?>())).Returns(auctions);
        _repositoryMock.Setup(r => r.AuctionRepository).Returns(auctionRepo.Object);

        var bids = new List<Bid>
        {
            new() { Amount = 100, IsDonated = true, User = admin, UserId = admin.Id, Auction = new Auction { Title = "Auction 1" }, AuctionId = Guid.NewGuid() },
            new() { Amount = 200, IsDonated = false, User = admin, UserId = admin.Id, Auction = new Auction { Title = "Auction 2" }, AuctionId = Guid.NewGuid() },
            new() { Amount = 300, IsDonated = true, User = admin, UserId = admin.Id, Auction = new Auction { Title = "Auction 1" }, AuctionId = Guid.NewGuid() }
        };

        _bidServiceMock.Setup(b => b.GetAllBidsWithDetailsAsync()).ReturnsAsync(bids);
        _bidServiceMock.Setup(b => b.GetMonthlySystemBidStatsAsync())
            .ReturnsAsync(new List<MonthlyBidStatDTO> { new() { Month = "Червень 2025", Count = 3 } });

        var service = CreateService();

        // Act
        var result = await service.GetOverviewAsync();

        // Assert
        result.Role.Should().Be("Admin");
        result.UserCount.Should().Be(1);
        result.AuctionCount.Should().BeGreaterThan(0);
        result.TotalBidAmount.Should().Be(600);
        result.AvgBidAmount.Should().BeApproximately(200, 0.01m);
        result.DonationCount.Should().Be(2);
        result.TopUsers.Should().HaveCount(1);
        result.TopAuctions.Should().HaveCountGreaterThan(0);
        result.MonthlyBids.Should().NotBeNull();
    }
}
