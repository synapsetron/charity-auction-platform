using System.Transactions;
using CharityAuction.Infrastructure.Persistence;
using CharityAuction.Infrastructure.Repositories.Interfaces;
using CharityAuction.Infrastructure.Repositories.Interfaces.Base;

namespace CharityAuction.Infrastructure.Repositories.Realizations.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationDbContext _context;

        private IAuctionRepository? _auctionRepository;
        private IBidRepository? _bidRepository;
        private INotificationRepository? _notificationRepository;

        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAuctionRepository AuctionRepository
        {
            get
            {
                if (_auctionRepository is null)
                {
                    _auctionRepository = new AuctionRepository(_context);
                }

                return _auctionRepository;
            }
        }

        public IBidRepository BidRepository
        {
            get
            {
                if (_bidRepository is null)
                {
                    _bidRepository = new BidRepository(_context);
                }

                return _bidRepository;
            }
        }
        public INotificationRepository NotificationRepository
        {
            get
            {
                if (_notificationRepository is null)
                {
                    _notificationRepository = new NotificationRepository(_context);
                }
                return _notificationRepository;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public TransactionScope BeginTransaction()
        {
            return new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
