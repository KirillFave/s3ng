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

public class GetUserByIdHandlerTests : IClassFixture<UserTestFixture>
{
    private readonly GetUserByIdHandler _handler;
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly IMapper _mapper;

    public GetUserByIdHandlerTests(UserTestFixture userFixture)
    {
        var serviceProvider = userFixture.ServiceProvider;
        _repositoryMock = new Mock<IUserRepository>();
        var logger = new LoggerConfiguration().MinimumLevel.Verbose().CreateLogger();
        _mapper = serviceProvider.GetService<IMapper>();
        _handler = new GetUserByIdHandler(_repositoryMock.Object, _mapper, logger);
    }

    [Fact]
    public async Task GetUserById_ReturnsNotFound()
    {
        //arrange
        _repositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None))
                                   .ReturnsAsync(null as User);
        var getUserReq = new GetUserByIdRequestDto { Id = Guid.NewGuid() };

        //Act
        var result = await _handler.Handle(getUserReq, CancellationToken.None);

        //Assert
        result.User.Should().BeNull();
        result.Result.Should().Be(GetUserResultModel.NotFound);
    }

    [Fact]
    public async Task GetUserById_ReturnsSuccess()
    {
        //arrange
        var userId = Guid.Parse("cae3cf0a-9a98-497e-9c53-dab9afd4f98b");
        var getUserReq = new GetUserByIdRequestDto { Id = userId };

        var fixture = new Fixture();
        var user = fixture.Build<User>()
            .With(u => u.AuthenticationId, Guid.Parse("568bbc91-9687-489b-851f-69729569d1c3"))
            .With(u => u.Id, userId)
            .Create();

        _repositoryMock.Setup(x => x.GetByIdAsync(userId, CancellationToken.None))
                                   .ReturnsAsync(user);

        //Act
        var result = await _handler.Handle(getUserReq, CancellationToken.None);

        //Assert
        result.User.Should().NotBeNull();
        result.User!.Id.Should().Be(userId.ToString());
        result.Result.Should().Be(GetUserResultModel.Success);
    }
}
