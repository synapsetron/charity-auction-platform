using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Persistence;
using CharityAuction.Infrastructure.Repositories.Interfaces;
using CharityAuction.Infrastructure.Repositories.Realizations.Base;

namespace CharityAuction.Infrastructure.Repositories
{
    public class BidRepository : RepositoryBase<Bid>, IBidRepository
    {
        public BidRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
