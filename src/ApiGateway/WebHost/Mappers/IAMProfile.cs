using AutoMapper;
using s3ng.Contracts.IAM;
using WebHost.Dto;

namespace WebHost.Mappers
{
    public class IAMProfile : Profile
    {
        public IAMProfile() 
        {
            CreateMap<RegistrationRequestDto, RegisterRequest>();
            CreateMap<AuthenticationRequestDto, AuthenticationRequest>();
        }
    }
}
