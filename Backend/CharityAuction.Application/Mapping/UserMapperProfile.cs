using AutoMapper;
using CharityAuction.Application.DTO.User;
using CharityAuction.Domain.Entities;


namespace CharityAuction.Application.Mapping
{
    public class UserMapperProfile : Profile
    {
      public UserMapperProfile()
      {
            CreateMap<ApplicationUser, UserResponseDTO>()
                .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.LockoutEnd.HasValue ? src.LockoutEnd.Value.UtcDateTime : (DateTime?)null));

            CreateMap<ApplicationUser, CurrentUserResponseDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

            CreateMap<UserRegisterDTO, ApplicationUser>();

            CreateMap<UpdateUserRequestDTO, ApplicationUser>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateUserRequestDTO, ApplicationUser>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}

