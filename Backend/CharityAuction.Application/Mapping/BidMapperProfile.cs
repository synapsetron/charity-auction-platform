using AutoMapper;
using CharityAuction.Application.DTO.Bid;
using CharityAuction.Domain.Entities;
namespace CharityAuction.Application.Mapping
{
    public class BidMapperProfile : Profile
    {
        public BidMapperProfile()
        {
            CreateMap<Bid, BidResponseDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.AuctionName, opt => opt.MapFrom(src => src.Auction.Title));

            CreateMap<CreateBidRequestDTO, Bid>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<Bid, BidInfoDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<Bid, BidResponseWithWinnerDTO>()
                .IncludeBase<Bid, BidResponseDTO>()
                .ForMember(dest => dest.isWinner, opt => opt.Ignore()) // визначається окремо
                .ForMember(dest => dest.isAuctionActive, opt => opt.MapFrom(src => src.Auction.IsActive))
                .ForMember(dest => dest.isAuctionSold, opt => opt.MapFrom(src => src.Auction.IsSold));
        }
    }
}
