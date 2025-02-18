using AutoMapper;
using s3ng.Contracts.IAM;
using WebHost.Dto.IAM;

namespace WebHost.Mappers
{
    public class IAMProfile : Profile
    {
        public IAMProfile() 
        {
            CreateMap<SharedLibrary.IAM.Enums.RoleType, RoleType>().ConvertUsing((value, destination) =>
            {
                switch (value)
                {
                    case SharedLibrary.IAM.Enums.RoleType.User:
                        return RoleType.User;
                    case SharedLibrary.IAM.Enums.RoleType.Admin:
                        return RoleType.Admin;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value));
                }
            });
            CreateMap<RegistrationRequestDto, RegisterRequest>();
            CreateMap<AuthenticationRequestDto, AuthenticationRequest>();
            CreateMap<RefreshTokenRequestDto, RefreshAccessTokenRequest>();
        }
    }
}
