using UserService.Domain.Entities;
using UserService.Application.Models;
using UserService.Application.Models.Results;
using static CreateUserResponse.Types;
using static DeleteUserResponse.Types;
using static GetUserResponse.Types;
using static UpdateUserResponse.Types;

namespace UserService.Application;

public static class Converter
{
    public static RoleDto ConvertRoleToModel(Role role)
    {
        return role switch
        {
            Role.Buyer => RoleDto.Buyer,
            Role.Seller => RoleDto.Seller,
            Role.Moderator => RoleDto.Moderator,
            _ => RoleDto.Unspecified,
        };
    }

    public static Role ConvertRoleToProto(RoleDto role)
    {
        return role switch
        {
            RoleDto.Buyer => Role.Buyer,
            RoleDto.Seller => Role.Seller,
            RoleDto.Moderator => Role.Moderator,
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

    public static RoleEntity ConvertRoleToEntity(RoleDto role)
    {
        return role switch
        {
            RoleDto.Buyer => RoleEntity.Buyer,
            RoleDto.Seller => RoleEntity.Seller,
            RoleDto.Moderator => RoleEntity.Moderator,
            _ => RoleEntity.Unspecified,
        };
    }
}