
using System.Transactions;

namespace CharityAuction.Infrastructure.Repositories.Interfaces.Base
{
    public interface IRepositoryWrapper
    {
        IAuctionRepository AuctionRepository { get; }
        IBidRepository BidRepository { get; }
        INotificationRepository NotificationRepository { get; }
        public int SaveChanges();
        public Task<int> SaveChangesAsync();
        public TransactionScope BeginTransaction();
    }
}
