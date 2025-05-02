using AutoMapper;
using CharityAuction.Application.DTO.Auction;
using CharityAuction.Application.DTO.Bid;
using CharityAuction.Domain.Entities;

namespace CharityAuction.Application.Mapping
{
    public class AuctionMapperProfile : Profile
    {
        public AuctionMapperProfile()
        {
            // Маппинг без ставок
            CreateMap<Auction, AuctionResponseDTO>();

            // Маппинг з ставками
            CreateMap<Auction, AuctionResponseWithBidsDTO>()
                .ForMember(dest => dest.Bids, opt => opt.MapFrom(src => src.Bids))
                .ForMember(dest => dest.isSold, opt => opt.MapFrom(src => src.IsSold));

            CreateMap<CreateAuctionRequestDTO, Auction>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => false)) // <-- ставим false
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(_ => false)) // <-- добавляем!
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<UpdateAuctionRequestDTO, Auction>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
