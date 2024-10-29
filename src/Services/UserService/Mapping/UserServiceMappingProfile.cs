﻿using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using UserService.EFCore.Entities;
using UserService.Models;
using UserService.Models.Requests;
using UserService.Models.Response;

namespace UserService.Mapping;

public class UserServiceMappingProfile : Profile
{
    public UserServiceMappingProfile()
    {
        CreateMap<GetUserByIdRequest, GetUserByIdRequestDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)));

        CreateMap<GetUserByAuthenticationIdRequest, GetUserByAuthenticationIdRequestDto>()
            .ForMember(dest => dest.AuthenticationId, opt => opt.MapFrom(src => Guid.Parse(src.AuthenticationId)));

        CreateMap<GetAllUsersRequest, GetAllUsersRequestDto>();
        CreateMap<DeleteUserRequest, DeleteUserRequestDto>();

        CreateMap<CreateUserRequest, CreateUserRequestDto>()
            .ForMember(dest => dest.AuthenticationId, opt => opt.MapFrom(src => src.AuthenticationId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Converter.ConvertRoleToModel(src.Role)));

        CreateMap<UpdateUserRequest, UpdateUserRequestDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<UserDto, UserInfo>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AuthenticationId, opt => opt.MapFrom(src => src.AuthenticationId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => Timestamp.FromDateTime(src.CreatedAt.ToUniversalTime())))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Converter.ConvertRoleToProto(src.Role)));
        
        CreateMap<GetUserResponseDto, GetUserResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => Converter.ConvertGetUserResult(src.Result)));

        CreateMap<GetAllUsersResponseDto, GetAllUsersResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Users))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => Converter.ConvertGetAllUsersResult(src.Result)));

        CreateMap<CreateUserResponseDto, CreateUserResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => Converter.ConvertCreateUserResult(src.Result)));

        CreateMap<UpdateUserResponseDto, UpdateUserResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => Converter.ConvertUpdateUserResult(src.Result)));

        CreateMap<DeleteUserResponseDto, DeleteUserResponse>()
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => Converter.ConvertDeleteUserResult(src.Result)));

        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(dest => dest.AuthenticationId, opt => opt.MapFrom(src => Guid.Parse(src.AuthenticationId)))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Converter.ConvertRoleToEntity(src.Role)))
            .ReverseMap();
    }
}