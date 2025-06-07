using CharityAuction.Application.DTO.Statistics;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Application.Interfaces;
using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Repositories.Interfaces.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class StatsService : IStatsService
{
    private readonly IRepositoryWrapper _repository;
    private readonly ICurrentUserService _currentUser;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBidService _bidService;

    public StatsService(
        IRepositoryWrapper repository,
        ICurrentUserService currentUser,
        IBidService bidService,
        UserManager<ApplicationUser> userManager)
    {
        _repository = repository;
        _currentUser = currentUser;
        _userManager = userManager;
        _bidService = bidService;
    }

    public async Task<StatsOverviewDTO> GetOverviewAsync()
    {
        var userId = _currentUser.GetUserId();
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new UnauthorizedAccessException("User not found");

        var role = user.Role;
        var dto = new StatsOverviewDTO { Role = role };

        if (role == "Seller")
        {
            var endedAuctionIds = await _repository.AuctionRepository
                .FindAll(a => !a.IsActive && a.EndTime <= DateTime.UtcNow)
                .Select(a => a.Id)
                .ToListAsync();

            var userBids = await _repository.BidRepository
                .FindAll(b => b.UserId == userId && endedAuctionIds.Contains(b.AuctionId))
                .ToListAsync();

            var wins = userBids
                .GroupBy(b => b.AuctionId)
                .Select(g => g.OrderByDescending(b => b.Amount).First())
                .Count();

            var myAuctions = await _repository.AuctionRepository
                .FindAll(a => a.OrganizerId == userId)
                .CountAsync();

            dto.MyWins = wins;
            dto.MyAuctions = myAuctions;
            dto.Balance = user.Balance;
        }

        if (role == "Admin")
        {
            dto.UserCount = await _userManager.Users.CountAsync();
            dto.AuctionCount = await _repository.AuctionRepository.FindAll().CountAsync();

            var allBids = (await _bidService.GetAllBidsWithDetailsAsync()).ToList();

            dto.MonthlyBids = await _bidService.GetMonthlySystemBidStatsAsync();


            dto.ActiveAuctionCount = await _repository.AuctionRepository
    .FindAll(a => a.IsActive)
    .CountAsync();

            dto.EndedAuctionCount = await _repository.AuctionRepository
                .FindAll(a => !a.IsActive && a.EndTime <= DateTime.UtcNow)
                .CountAsync();

            dto.TotalBidAmount = allBids.Sum(b => b.Amount);
            dto.AvgBidAmount = allBids.Any() ? allBids.Average(b => b.Amount) : 0;
            dto.DonationCount = allBids.Count(b => b.IsDonated);

            // Top 5 most active auctions
            dto.TopAuctions = allBids
                .GroupBy(b => b.AuctionId)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new PopularAuctionStatDTO
                {
                    AuctionId = g.Key,
                    AuctionName = g.First().Auction.Title,
                    BidCount = g.Count()
                });

            // Top 5 users by number of bids
            dto.TopUsers = allBids
                .GroupBy(b => b.UserId)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new TopUserStatDTO
                {
                    UserId = g.Key,
                    UserName = g.First().User.FirstName + " " + g.First().User.LastName,
                    TotalBids = g.Count()
                });
        }
        return dto;
    }
}
