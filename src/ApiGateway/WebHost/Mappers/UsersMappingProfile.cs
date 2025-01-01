using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using SharedLibrary.UserService.Models;

namespace WebHost.Mappers
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<UpdateUserRequestModel, UpdateUserRequest>();
            CreateMap<CreateUserRequestModel, CreateUserRequest>();

            CreateMap<UserModel, UserInfo>().ReverseMap();

            CreateMap<Timestamp, DateTime>().ConvertUsing(src => src.ToDateTime());
            CreateMap<DateTime, Timestamp>().ConvertUsing(src => Timestamp.FromDateTime(src));

            CreateMap<RoleModel, Role>().ConvertUsing(src => Converter.MapRoleDtoToRole(src));
            CreateMap<Role, RoleModel>().ConvertUsing(src => Converter.MapRoleToRoleDto(src));
        }
    }

    public static class Converter
    {
        public static Role MapRoleDtoToRole(RoleModel role)
        {
            return role switch
            {
                RoleModel.Buyer => Role.Buyer,
                RoleModel.Seller => Role.Seller,
                RoleModel.Moderator => Role.Moderator,
                _ => Role.Unspecified,
            };
        }

        public static RoleModel MapRoleToRoleDto(Role role)
        {
            return role switch
            {
                Role.Buyer => RoleModel.Buyer,
                Role.Seller => RoleModel.Seller,
                Role.Moderator => RoleModel.Moderator,
                _ => RoleModel.Unspecified,
            };
        }
    }
}
