using AutoMapper;
using s3ng.Contracts.IAM;
using WebHost.Dto;

namespace WebHost.Mappers
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile() 
        {
            CreateMap<RegistrationRequestDto, RegisterRequest>();
        }
    }
}
