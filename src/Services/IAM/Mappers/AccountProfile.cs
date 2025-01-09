using AutoMapper;
using IAM.Entities;
using IAM.Models;

namespace IAM.Mappers
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<User, AccountModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash)).ReverseMap();
        }
    }
}
