using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using WebHost.Dto.User;

namespace WebHost.Mappers
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<UpdateUserRequestDto, UpdateUserRequest>();
            CreateMap<CreateUserRequestDto, CreateUserRequest>();

            CreateMap<UserDto, UserInfo>().ReverseMap();

            CreateMap<Timestamp, DateTime>().ConvertUsing(src => src.ToDateTime());
            CreateMap<DateTime, Timestamp>().ConvertUsing(src => Timestamp.FromDateTime(src));

            CreateMap<RoleDto, Role>().ConvertUsing(src => Converter.MapRoleDtoToRole(src));
            CreateMap<Role, RoleDto>().ConvertUsing(src => Converter.MapRoleToRoleDto(src));
        }
    }

    public static class Converter
    {
        public static Role MapRoleDtoToRole(RoleDto role)
        {
            return role switch
            {
                RoleDto.Buyer => Role.Buyer,
                RoleDto.Seller => Role.Seller,
                RoleDto.Moderator => Role.Moderator,
                _ => Role.Unspecified,
            };
        }

        public static RoleDto MapRoleToRoleDto(Role role)
        {
            return role switch
            {
                Role.Buyer => RoleDto.Buyer,
                Role.Seller => RoleDto.Seller,
                Role.Moderator => RoleDto.Moderator,
                _ => RoleDto.Unspecified,
            };
        }
    }
}
