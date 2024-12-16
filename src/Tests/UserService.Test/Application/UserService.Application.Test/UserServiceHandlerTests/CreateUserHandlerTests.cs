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

public class CreateUserHandlerTests : IClassFixture<UserTestFixture>
{
    private readonly CreateUserHandler _handler;
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly IMapper _mapper;

    public CreateUserHandlerTests(UserTestFixture userFixture)
    {
        var serviceProvider = userFixture.ServiceProvider;
        _repositoryMock = new Mock<IUserRepository>();
        var logger = new LoggerConfiguration().MinimumLevel.Verbose().CreateLogger();
        _mapper = serviceProvider.GetService<IMapper>();
        _handler = new CreateUserHandler(_repositoryMock.Object, _mapper, logger);
    }

    [Fact]
    public async Task CreateUser_ReturnsFail()
    {
        //arrange
        var fixture = new Fixture();
        var createUserReq = fixture.Build<CreateUserRequestDto>()
            .With(u => u.AuthenticationId, "568bbc91-9687-489b-851f-69729569d1c3")
            .Create();

        _repositoryMock.Setup(x => x.AddAsync(It.IsAny<User>(), CancellationToken.None))
                                   .ReturnsAsync(null as User);
        
        //Act
        var result = await _handler.Handle(createUserReq, CancellationToken.None);

        //Assert
        result.User.Should().BeNull();
        result.Result.Should().Be(CreateUserResultModel.Fail);
    }

    [Fact]
    public async Task CreateUser_ReturnsSuccess()
    {
        //arrange
        var fixture = new Fixture();
        var createUserReq = fixture.Build<CreateUserRequestDto>()
            .With(u => u.AuthenticationId, "568bbc91-9687-489b-851f-69729569d1c3")
            .Create();

        var userEntity = _mapper.Map<User>(createUserReq);
        _repositoryMock.Setup(x => x.AddAsync(It.IsAny<User>(), CancellationToken.None))
                                   .ReturnsAsync(userEntity);

        //Act
        var result = await _handler.Handle(createUserReq, CancellationToken.None);

        //Assert
        result.User.Should().NotBeNull();
        result.User!.Address.Should().Be(createUserReq.Address);
        result.User!.FirstName.Should().Be(createUserReq.FirstName);
        result.User!.LastName.Should().Be(createUserReq.LastName);
        result.User!.Role.Should().Be(createUserReq.Role);
        result.User!.PhoneNumber.Should().Be(createUserReq.PhoneNumber);
        result.User!.AuthenticationId.Should().Be(createUserReq.AuthenticationId);
        result.Result.Should().Be(CreateUserResultModel.Success);
    }
}
