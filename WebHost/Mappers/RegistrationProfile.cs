using AutoMapper;
using s3ng.Contracts.IAM;
using s3ng.WebHost.Dto;

namespace s3ng.WebHost.Mappers
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile() 
        {
            CreateMap<RegistrationRequestDto, RegisterRequest>();
        }
    }
}
