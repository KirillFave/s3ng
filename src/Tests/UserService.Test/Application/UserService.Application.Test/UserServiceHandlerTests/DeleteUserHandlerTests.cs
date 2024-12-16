using FluentAssertions;
using Moq;
using Serilog;
using UserService.Application.Handler;
using UserService.Application.Models.Requests;
using UserService.Application.Models.Results;
using UserService.Infrastructure.Repository;
using Xunit;

namespace UserService.Application.Test.UserServiceHandlerTests;

public class DeleteUserHandlerTests
{
    private readonly DeleteUserHandler _handler;
    private readonly Mock<IUserRepository> _repositoryMock;

    public DeleteUserHandlerTests()
    {
        _repositoryMock = new Mock<IUserRepository>();
        var logger = new LoggerConfiguration().MinimumLevel.Verbose().CreateLogger();
        _handler = new DeleteUserHandler(_repositoryMock.Object, logger);
    }

    [Fact]
    public async Task DeleteUser_ReturnsFail()
    {
        //arrange
        _repositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), CancellationToken.None))
                                   .ReturnsAsync(false);
        var deleteUserReq = new DeleteUserRequestDto { Id = Guid.NewGuid() };

        //Act
        var result = await _handler.Handle(deleteUserReq, CancellationToken.None);

        //Assert
        result.Result.Should().Be(DeleteUserResultModel.Fail);
    }

    [Fact]
    public async Task DeleteUser_ReturnsSuccess()
    {
        //arrange
        _repositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), CancellationToken.None))
                                   .ReturnsAsync(true);
        var deleteUserReq = new DeleteUserRequestDto { Id = Guid.NewGuid() };

        //Act
        var result = await _handler.Handle(deleteUserReq, CancellationToken.None);

        //Assert
        result.Result.Should().Be(DeleteUserResultModel.Success);
    }
}
