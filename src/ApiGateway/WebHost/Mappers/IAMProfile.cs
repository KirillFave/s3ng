using AutoMapper;
using s3ng.Contracts.IAM;
using WebHost.Dto.IAM;

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
