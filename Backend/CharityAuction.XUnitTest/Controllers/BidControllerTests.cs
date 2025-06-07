using CharityAuction.Application.DTO.Bid;
using CharityAuction.Application.Interfaces;
using CharityAuction.WebAPI.Controllers;
using CharityAuction.WebAPI.Hubs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CharityAuction.API.Tests.Controllers;

public class BidControllerTests
{
    private readonly Mock<IBidService> _bidService = new();
    private readonly Mock<ILogger<BidController>> _logger = new();
    private readonly Mock<IHubContext<AuctionHub>> _hubContext = new();
    private readonly Mock<IClientProxy> _clientProxy = new();
    private readonly BidController _controller;

    public BidControllerTests()
    {
        var clients = new Mock<IHubClients>();
        var groupManager = new Mock<IClientProxy>();
        clients.Setup(x => x.Group(It.IsAny<string>())).Returns(_clientProxy.Object);

        _hubContext.Setup(x => x.Clients).Returns(clients.Object);
        _controller = new BidController(_bidService.Object, _logger.Object, _hubContext.Object);
    }

    [Fact]
    public async Task PlaceBid_ReturnsCreated()
    {
        var dto = new BidResponseDTO { Id = Guid.NewGuid(), AuctionId = Guid.NewGuid() };
        _bidService.Setup(s => s.PlaceBidAsync(It.IsAny<CreateBidRequestDTO>())).ReturnsAsync(dto);

        var result = await _controller.PlaceBid(new CreateBidRequestDTO());

        var created = result as CreatedAtActionResult;
        created!.StatusCode.Should().Be(201);
        created.Value.Should().Be(dto);
    }

    [Fact]
    public async Task PlaceBid_InvalidOperationException_ReturnsBadRequest()
    {
        _bidService.Setup(s => s.PlaceBidAsync(It.IsAny<CreateBidRequestDTO>()))
            .ThrowsAsync(new InvalidOperationException("invalid"));

        var result = await _controller.PlaceBid(new CreateBidRequestDTO());

        var badRequest = result as BadRequestObjectResult;
        badRequest!.Value.Should().Be("invalid");
    }

    [Fact]
    public async Task PlaceBid_UnauthorizedAccessException_Returns401()
    {
        _bidService.Setup(s => s.PlaceBidAsync(It.IsAny<CreateBidRequestDTO>()))
            .ThrowsAsync(new UnauthorizedAccessException("unauth"));

        var result = await _controller.PlaceBid(new CreateBidRequestDTO());

        var unauthorized = result as UnauthorizedObjectResult;
        unauthorized!.Value.Should().Be("unauth");
    }

    [Fact]
    public async Task PlaceBid_Exception_Returns500()
    {
        _bidService.Setup(s => s.PlaceBidAsync(It.IsAny<CreateBidRequestDTO>()))
            .ThrowsAsync(new Exception("fail"));

        var result = await _controller.PlaceBid(new CreateBidRequestDTO());

        var error = result as ObjectResult;
        error!.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task GetHighestBid_ReturnsOk()
    {
        _bidService.Setup(s => s.GetHighestBidAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new BidResponseDTO());

        var result = await _controller.GetHighestBid(Guid.NewGuid());

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetHighestBid_ReturnsNotFound()
    {
        _bidService.Setup(s => s.GetHighestBidAsync(It.IsAny<Guid>())).ReturnsAsync((BidResponseDTO?)null);

        var result = await _controller.GetHighestBid(Guid.NewGuid());

        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task GetAllBidsForAuction_ReturnsOk()
    {
        _bidService.Setup(s => s.GetAllBidsForAuctionAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<BidResponseDTO> { new() });

        var result = await _controller.GetAllBidsForAuction(Guid.NewGuid());

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetAllBidsForAuction_EmptyList_ReturnsNotFound()
    {
        _bidService.Setup(s => s.GetAllBidsForAuctionAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<BidResponseDTO>());

        var result = await _controller.GetAllBidsForAuction(Guid.NewGuid());

        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task GetUserBids_ReturnsOk()
    {
        _bidService.Setup(s => s.GetUserBidsAsync()).ReturnsAsync(new List<BidResponseDTO>());

        var result = await _controller.GetUserBids();

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetUserBids_Exception_Returns500()
    {
        _bidService.Setup(s => s.GetUserBidsAsync()).ThrowsAsync(new Exception("fail"));

        var result = await _controller.GetUserBids();

        var obj = result as ObjectResult;
        obj!.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task GetUserBidsByUserId_ReturnsOk()
    {
        _bidService.Setup(s => s.GetUserBidsByUserIdAsync("123")).ReturnsAsync(new List<BidResponseDTO>());

        var result = await _controller.GetUserBidsByUserId("123");

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetUserBidsByUserId_Empty_ReturnsBadRequest()
    {
        var result = await _controller.GetUserBidsByUserId("");

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task GetBidById_ReturnsOk()
    {
        _bidService.Setup(s => s.GetBidByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new BidResponseDTO());

        var result = await _controller.GetBidById(Guid.NewGuid());

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetBidById_ReturnsNotFound()
    {
        _bidService.Setup(s => s.GetBidByIdAsync(It.IsAny<Guid>())).ReturnsAsync((BidResponseDTO?)null);

        var result = await _controller.GetBidById(Guid.NewGuid());

        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task DonateBid_Success_ReturnsOk()
    {
        _bidService.Setup(s => s.DonateBidAsync(It.IsAny<Guid>())).ReturnsAsync(true);

        var result = await _controller.DonateBid(Guid.NewGuid());

        var ok = result as OkObjectResult;
        ok!.Value.Should().Be("Bid successfully donated.");
    }

    [Fact]
    public async Task DonateBid_Failed_ReturnsBadRequest()
    {
        _bidService.Setup(s => s.DonateBidAsync(It.IsAny<Guid>())).ReturnsAsync(false);

        var result = await _controller.DonateBid(Guid.NewGuid());

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task DonateBid_Unauthorized_Returns401()
    {
        _bidService.Setup(s => s.DonateBidAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new UnauthorizedAccessException("unauth"));

        var result = await _controller.DonateBid(Guid.NewGuid());

        result.Should().BeOfType<UnauthorizedObjectResult>();
    }

    [Fact]
    public async Task GetUserBidHistory_ReturnsOk()
    {
        var data = new List<BidResponseWithWinnerDTO>();

        _bidService.Setup(s => s.GetUserBidHistoryAsync())
                   .ReturnsAsync((IEnumerable<BidResponseWithWinnerDTO>)data);

        var result = await _controller.GetUserBidHistory();

        result.Should().BeOfType<OkObjectResult>();
    }



    [Fact]
    public async Task GetUserWinningBids_Null_ReturnsNotFound()
    {
        _bidService.Setup(s => s.GetUserWinningBidsAsync())
        .ReturnsAsync((IEnumerable<BidResponseDTO>?)null);

        var result = await _controller.GetUserWinningBids();

        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task GetMonthlyStats_ReturnsOk()
    {
        _bidService.Setup(s => s.GetMonthlyUserBidStatsAsync())
            .ReturnsAsync(new List<MonthlyBidStatDTO>());

        var result = await _controller.GetMonthlyStats();

        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetSystemMonthlyStats_ReturnsOk()
    {
        _bidService.Setup(s => s.GetMonthlySystemBidStatsAsync())
            .ReturnsAsync(new List<MonthlyBidStatDTO>());

        var result = await _controller.GetSystemMonthlyStats();

        result.Should().BeOfType<OkObjectResult>();
    }
}
