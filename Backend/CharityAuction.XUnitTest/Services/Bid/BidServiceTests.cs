using AutoMapper;
using CharityAuction.Application.DTO.Bid;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Application.Services;
using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Repositories.Interfaces.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace CharityAuction.XUnitTest.Services.BidTest;
public class BidServiceTests
{
    private readonly Mock<IRepositoryWrapper> _repoMock = new();
    private readonly Mock<ICurrentUserService> _userMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ILogger<BidService>> _loggerMock = new();
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly BidService _sut;

    public BidServiceTests()
    {
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            userStoreMock.Object,
            null, null,
            new IUserValidator<ApplicationUser>[0],
            new IPasswordValidator<ApplicationUser>[0],
            null, null, null, null);

        _sut = new BidService(
            _repoMock.Object,
            _userMock.Object,
            _mapperMock.Object,
            _loggerMock.Object,
            _userManagerMock.Object);
    }

    private CreateBidRequestDTO CreateValidBidRequest(Guid? auctionId = null, decimal amount = 100) =>
        new CreateBidRequestDTO { AuctionId = auctionId ?? Guid.NewGuid(), Amount = amount };

    private Auction CreateActiveAuction(Guid auctionId, decimal startingPrice = 10) =>
        new Auction { Id = auctionId, IsActive = true, StartingPrice = startingPrice };

    private void SetupAuction(Auction auction) =>
        _repoMock.Setup(r => r.AuctionRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Auction, bool>>>(), null)).ReturnsAsync(auction);

    private void SetupBids(params Bid[] bids) =>
        _repoMock.Setup(r => r.BidRepository.FindAll(It.IsAny<Expression<Func<Bid, bool>>>()))
            .Returns(bids.AsQueryable());

    private void SetupUser(string userId, ApplicationUser? user = null)
    {
        _userMock.Setup(u => u.GetUserId()).Returns(userId);
        _userManagerMock.Setup(m => m.FindByIdAsync(userId)).ReturnsAsync(user);
    }

    private void SetupMapping(Bid bid, BidResponseDTO response)
    {
        _mapperMock.Setup(m => m.Map<Bid>(It.IsAny<CreateBidRequestDTO>())).Returns(bid);
        _mapperMock.Setup(m => m.Map<BidResponseDTO>(It.IsAny<Bid>())).Returns(response);
    }

    [Fact(DisplayName = "Should throw if request is null")]
    public async Task PlaceBidAsync_ShouldThrow_IfRequestIsNull()
    {
        Func<Task> act = async () => await _sut.PlaceBidAsync(null!);
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("request");
    }

    [Fact(DisplayName = "Should throw if amount is negative or zero")]
    public async Task PlaceBidAsync_ShouldThrow_IfAmountNotPositive()
    {
        var dto = CreateValidBidRequest(amount: 0);
        Func<Task> act = async () => await _sut.PlaceBidAsync(dto);
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*Bid amount must be positive*");
    }

    [Fact(DisplayName = "Should fetch auction by ID")]
    public async Task PlaceBidAsync_ShouldFetchAuctionById()
    {
        var dto = CreateValidBidRequest();
        var auction = CreateActiveAuction(dto.AuctionId);
        SetupAuction(auction);
        SetupBids();
        SetupUser("user-1", new ApplicationUser());
        SetupMapping(new Bid(), new BidResponseDTO());
        var result = await _sut.PlaceBidAsync(dto);
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "Should throw if auction is inactive")]
    public async Task PlaceBidAsync_ShouldThrow_IfAuctionIsInactive()
    {
        var dto = CreateValidBidRequest();
        SetupAuction(new Auction { IsActive = false });
        Func<Task> act = async () => await _sut.PlaceBidAsync(dto);
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*Auction not found or inactive*");
    }

    [Fact(DisplayName = "Should throw if bid is less than minimum allowed")]
    public async Task PlaceBidAsync_ShouldThrow_IfBidIsTooLow()
    {
        var dto = CreateValidBidRequest(amount: 10);
        var auction = CreateActiveAuction(dto.AuctionId, 50);
        SetupAuction(auction);
        SetupBids(new Bid { Amount = 60 });
        Func<Task> act = async () => await _sut.PlaceBidAsync(dto);
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*Bid must be at least*");
    }

    [Fact(DisplayName = "Should throw if user is not authenticated")]
    public async Task PlaceBidAsync_ShouldThrow_IfUserIdMissing()
    {
        var dto = CreateValidBidRequest();
        SetupAuction(CreateActiveAuction(dto.AuctionId));
        SetupBids();
        _userMock.Setup(u => u.GetUserId()).Returns(string.Empty);
        Func<Task> act = async () => await _sut.PlaceBidAsync(dto);
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact(DisplayName = "Should throw if user not found")]
    public async Task PlaceBidAsync_ShouldThrow_IfUserNotFound()
    {
        var dto = CreateValidBidRequest();
        SetupAuction(CreateActiveAuction(dto.AuctionId));
        SetupBids();
        SetupUser("user-1", null);
        Func<Task> act = async () => await _sut.PlaceBidAsync(dto);
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact(DisplayName = "Should call CreateAsync on repository")]
    public async Task PlaceBidAsync_ShouldCallCreateAsync()
    {
        var dto = CreateValidBidRequest();
        var auction = CreateActiveAuction(dto.AuctionId);
        SetupAuction(auction);
        SetupBids();
        SetupUser("user-1", new ApplicationUser());
        SetupMapping(new Bid(), new BidResponseDTO());
        await _sut.PlaceBidAsync(dto);
        _repoMock.Verify(r => r.BidRepository.CreateAsync(It.IsAny<Bid>()), Times.Once);
    }

    [Fact(DisplayName = "Should call SaveChangesAsync after creating bid")]
    public async Task PlaceBidAsync_ShouldSaveChanges()
    {
        var dto = CreateValidBidRequest();
        var auction = CreateActiveAuction(dto.AuctionId);
        SetupAuction(auction);
        SetupBids();
        SetupUser("user-1", new ApplicationUser());
        SetupMapping(new Bid(), new BidResponseDTO());
        await _sut.PlaceBidAsync(dto);
        _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact(DisplayName = "Should log successful bid placement")]
    public async Task PlaceBidAsync_ShouldLogInformation()
    {
        var dto = CreateValidBidRequest();
        var auction = CreateActiveAuction(dto.AuctionId);
        SetupAuction(auction);
        SetupBids();
        SetupUser("user-1", new ApplicationUser());
        SetupMapping(new Bid(), new BidResponseDTO());
        await _sut.PlaceBidAsync(dto);
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Bid successfully placed")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact(DisplayName = "Should return mapped BidResponseDTO")]
    public async Task PlaceBidAsync_ShouldReturnMappedResponse()
    {
        var dto = CreateValidBidRequest();
        var response = new BidResponseDTO { Id = Guid.NewGuid() };
        SetupAuction(CreateActiveAuction(dto.AuctionId));
        SetupBids();
        SetupUser("user-1", new ApplicationUser());
        SetupMapping(new Bid(), response);
        var result = await _sut.PlaceBidAsync(dto);
        result.Should().BeEquivalentTo(response);
    }

    [Fact(DisplayName = "Should allow bid equal to minimum amount")]
    public async Task PlaceBidAsync_ShouldAllow_ExactMinimumBid()
    {
        var dto = CreateValidBidRequest(amount: 51);
        var auction = CreateActiveAuction(dto.AuctionId, 50);
        SetupAuction(auction);
        SetupBids(new Bid { Amount = 50 });
        SetupUser("user-1", new ApplicationUser());
        SetupMapping(new Bid(), new BidResponseDTO());
        var result = await _sut.PlaceBidAsync(dto);
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "Should handle case when auction is null")]
    public async Task PlaceBidAsync_ShouldThrow_WhenAuctionIsNull()
    {
        var dto = CreateValidBidRequest();
        SetupAuction(null!);
        Func<Task> act = async () => await _sut.PlaceBidAsync(dto);
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("*Auction not found or inactive*");
    }

    [Fact(DisplayName = "Should throw if UserManager throws exception")]
    public async Task PlaceBidAsync_ShouldThrow_IfUserManagerFails()
    {
        var dto = CreateValidBidRequest();
        SetupAuction(CreateActiveAuction(dto.AuctionId));
        SetupBids();
        _userMock.Setup(u => u.GetUserId()).Returns("user-1");
        _userManagerMock.Setup(m => m.FindByIdAsync("user-1")).ThrowsAsync(new Exception("UserManager failed"));
        Func<Task> act = async () => await _sut.PlaceBidAsync(dto);
        await act.Should().ThrowAsync<Exception>().WithMessage("*UserManager failed*");
    }

    [Fact(DisplayName = "Should create Bid with correct CreatedAt")]
    public async Task PlaceBidAsync_ShouldSet_CreatedAt()
    {
        var dto = CreateValidBidRequest();
        var bid = new Bid();
        SetupAuction(CreateActiveAuction(dto.AuctionId));
        SetupBids();
        SetupUser("user-1", new ApplicationUser());
        SetupMapping(bid, new BidResponseDTO());
        await _sut.PlaceBidAsync(dto);
        bid.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact(DisplayName = "Should assign ApplicationUser to Bid")]
    public async Task PlaceBidAsync_ShouldAssignUser()
    {
        var dto = CreateValidBidRequest();
        var user = new ApplicationUser { Id = "user-1" };
        var bid = new Bid();
        SetupAuction(CreateActiveAuction(dto.AuctionId));
        SetupBids();
        SetupUser(user.Id, user);
        SetupMapping(bid, new BidResponseDTO());
        await _sut.PlaceBidAsync(dto);
        bid.User.Should().BeSameAs(user);
    }
}