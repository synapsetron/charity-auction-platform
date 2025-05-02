using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Persistence;
using CharityAuction.Infrastructure.Repositories.Interfaces;
using CharityAuction.Infrastructure.Repositories.Realizations.Base;

namespace CharityAuction.Infrastructure.Repositories.Realizations
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
