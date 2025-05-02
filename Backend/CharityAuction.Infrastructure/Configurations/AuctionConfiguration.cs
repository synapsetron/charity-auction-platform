using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CharityAuction.Domain.Entities;

namespace CharityAuction.Infrastructure.Persistence.Configurations
{
    public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .IsRequired();

            builder.Property(a => a.StartingPrice)
                .IsRequired();

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.HasOne(a => a.Organizer)
                .WithMany(u => u.Auctions)
                .HasForeignKey(a => a.OrganizerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
