
using CharityAuction.Application.DTO.Statistics;

namespace CharityAuction.Application.Interfaces
{
    public interface IStatsService
    {
        Task<StatsOverviewDTO> GetOverviewAsync();
    }
}
