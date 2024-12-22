using UserService.Domain.Entities;
using UserService.Application.Models;
using UserService.Application.Models.Results;
using static CreateUserResponse.Types;
using static DeleteUserResponse.Types;
using static GetUserResponse.Types;
using static UpdateUserResponse.Types;
using SharedLibrary.UserService.Models;

namespace UserService.Application;

public static class Converter
{
    public static RoleModel ConvertRoleToModel(Role role)
    {
        return role switch
        {
            Role.Buyer => RoleModel.Buyer,
            Role.Seller => RoleModel.Seller,
            Role.Moderator => RoleModel.Moderator,
            _ => RoleModel.Unspecified,
        };
    }

    public static Role ConvertRoleToProto(RoleModel role)
    {
        return role switch
        {
            RoleModel.Buyer => Role.Buyer,
            RoleModel.Seller => Role.Seller,
            RoleModel.Moderator => Role.Moderator,
            _ => Role.Unspecified,
        };
    }

    public static GetUserResult ConvertGetUserResult(GetUserResultModel result)
    {
        return result switch
        {
            GetUserResultModel.Fail => GetUserResult.Fail,
            GetUserResultModel.Success => GetUserResult.Success,
            GetUserResultModel.NotFound => GetUserResult.NotFound,
            _ => GetUserResult.Unspecified,
        };
    }

    public static CreateUserResult ConvertCreateUserResult(CreateUserResultModel result)
    {
        return result switch
        {
            CreateUserResultModel.Fail => CreateUserResult.Fail,
            CreateUserResultModel.Success => CreateUserResult.Success,
            _ => CreateUserResult.Unspecified,
        };
    }

    public static UpdateUserResult ConvertUpdateUserResult(UpdateUserResultModel result)
    {
        return result switch
        {
            UpdateUserResultModel.Fail => UpdateUserResult.Fail,
            UpdateUserResultModel.Success => UpdateUserResult.Success,
            UpdateUserResultModel.NotFound => UpdateUserResult.NotFound,
            _ => UpdateUserResult.Unspecified,
        };
    }

    public static DeleteUserResult ConvertDeleteUserResult(DeleteUserResultModel result)
    {
        return result switch
        {
            DeleteUserResultModel.Fail => DeleteUserResult.Fail,
            DeleteUserResultModel.Success => DeleteUserResult.Success,
            DeleteUserResultModel.NotFound => DeleteUserResult.NotFound,
            _ => DeleteUserResult.Unspecified,
        };
    }

    public static RoleEntity ConvertRoleToEntity(RoleModel role)
    {
        return role switch
        {
            RoleModel.Buyer => RoleEntity.Buyer,
            RoleModel.Seller => RoleEntity.Seller,
            RoleModel.Moderator => RoleEntity.Moderator,
            _ => RoleEntity.Unspecified,
        };
    }
}
