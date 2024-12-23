using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Serilog;
using UserService.Application.Handler;
using UserService.Application.Models.Requests;
using UserService.Application.Models.Results;
using UserService.Domain.Entities;
using UserService.Infrastructure.Repository;
using Xunit;

namespace UserService.Application.Test.UserServiceHandlerTests;

public class UpdateUserHandlerTests : IClassFixture<UserTestFixture>
{
    private readonly UpdateUserHandler _handler;
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly IMapper _mapper;

    public UpdateUserHandlerTests(UserTestFixture userFixture)
    {
        var serviceProvider = userFixture.ServiceProvider;
        _repositoryMock = new Mock<IUserRepository>();
        var logger = new LoggerConfiguration().MinimumLevel.Verbose().CreateLogger();
        _mapper = serviceProvider.GetService<IMapper>();
        _handler = new UpdateUserHandler(_repositoryMock.Object, _mapper, logger);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNotFound()
    {
        //arrange
        _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None))
                                   .ReturnsAsync(null as User);
        
        var fixture = new Fixture();
        var updateUserReq = fixture.Build<UpdateUserRequestDto>()
            .With(u => u.Id, Guid.Parse("568bbc91-9687-489b-851f-69729569d1c3"))
            .Create();

        //Act
        var result = await _handler.Handle(updateUserReq, CancellationToken.None);

        //Assert
        result.User.Should().BeNull();
        result.Result.Should().Be(UpdateUserResultModel.NotFound);
    }

    [Fact]
    public async Task CreateUser_ReturnsFail()
    {
        //arrange
        var fixture = new Fixture();
        var userId = Guid.Parse("cae3cf0a-9a98-497e-9c53-dab9afd4f98b");
        var updateUserReq = fixture.Build<UpdateUserRequestDto>()
            .With(u => u.Id, userId)
            .Create();

        var userEntity = fixture.Build<User>()
            .With(u => u.Id, userId)
            .With(u => u.AuthenticationId, Guid.Parse("fb135cd2-e48e-40f9-a844-4de31473bc6f"))
            .Create();

        _repositoryMock.Setup(x => x.GetByIdAsync(userId, CancellationToken.None))
                                   .ReturnsAsync(userEntity);

        _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<User>(), CancellationToken.None))
                                   .ReturnsAsync(null as User);

        //Act
        var result = await _handler.Handle(updateUserReq, CancellationToken.None);

        //Assert
        result.User.Should().BeNull();
        result.Result.Should().Be(UpdateUserResultModel.Fail);
    }

    [Fact]
    public async Task CreateUser_ReturnsSuccess()
    {
        //arrange
        var fixture = new Fixture();
        var userId = Guid.Parse("cae3cf0a-9a98-497e-9c53-dab9afd4f98b");
        var updateUserReq = fixture.Build<UpdateUserRequestDto>()
            .With(u => u.Id, userId)
            .Create();

        var userEntity = fixture.Build<User>()
            .With(u => u.Id, userId)
            .With(u => u.AuthenticationId, Guid.Parse("fb135cd2-e48e-40f9-a844-4de31473bc6f"))
            .Create();

        _repositoryMock.Setup(x => x.GetByIdAsync(userId, CancellationToken.None))
                                   .ReturnsAsync(userEntity);

        var newUserEntity = GetUpdateUser(updateUserReq, userEntity);
        _repositoryMock.Setup(x => x.UpdateAsync(newUserEntity, CancellationToken.None))
                                   .ReturnsAsync(newUserEntity);

        //Act
        var result = await _handler.Handle(updateUserReq, CancellationToken.None);

        //Assert
        result.User.Should().NotBeNull();
        result.User!.Address.Should().Be(newUserEntity.Address);
        result.User!.FirstName.Should().Be(newUserEntity.FirstName);
        result.User!.LastName.Should().Be(newUserEntity.LastName);
        result.User!.PhoneNumber.Should().Be(newUserEntity.Phone);
        result.Result.Should().Be(UpdateUserResultModel.Success);
    }

    private User GetUpdateUser(UpdateUserRequestDto dto, User user)
    {
        user.FirstName = string.IsNullOrEmpty(dto.FirstName) ? user.FirstName : dto.FirstName;
        user.LastName = string.IsNullOrEmpty(dto.LastName) ? user.LastName : dto.LastName;
        user.Address = string.IsNullOrEmpty(dto.Address) ? user.Address : dto.Address;
        user.Phone = dto.PhoneNumber > 0 ? dto.PhoneNumber : user.Phone;

        return user;
    }
}
