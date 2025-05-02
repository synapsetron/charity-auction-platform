using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Persistence;
using CharityAuction.Infrastructure.Repositories.Interfaces.Base;
using CharityAuction.Infrastructure.Repositories.Interfaces;
using CharityAuction.Infrastructure.Repositories.Realizations.Base;

namespace CharityAuction.Infrastructure.Repositories
{
    public class AuctionRepository : RepositoryBase<Auction>, IAuctionRepository
    {
        public AuctionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
