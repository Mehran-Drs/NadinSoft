using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NadinSoft.API.Controllers;
using NadinSoft.Application.Common;
using NadinSoft.Application.CQRS.Authentication.Command;
using NadinSoft.Application.CQRS.Authentication.Query;

namespace NodinSoft.Test.Systems.Controllers
{
    public class AccountControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AccountController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnCreated_WhenSuccessful()
        {
            //Arrange
            var registerCommand = new RegisterUserCommand();
            var result = new Result<int> { IsSuccessful = true };
            _mediatorMock.Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), default)).ReturnsAsync(result);

            // Act
            var actionResult = await _controller.Register(registerCommand);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(result, createdResult.Value);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenUnsuccessful()
        {
            //Arrange
            var registerCommand = new RegisterUserCommand();
            var result = new Result<int> { IsSuccessful = false };
            _mediatorMock.Setup(m => m.Send(registerCommand, default)).ReturnsAsync(result);

            //Act
            var actionResult = await _controller.Register(registerCommand);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal(result, badRequestResult.Value);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenSuccessful()
        {
            //Arrange 
            var loginQuery = new LoginQuery();
            var result = new Result<LoginQueryResult>() { IsSuccessful = true };

            _mediatorMock.Setup(m=>m.Send(It.IsAny<LoginQuery>(), default)).ReturnsAsync(result);

            //Act
            var loginAction = await _controller.Login(loginQuery);

            //Assert
            var okResult= Assert.IsType<OkObjectResult>(loginAction);

            Assert.Equal(result, okResult.Value);
        }

        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenUnsuccesful()
        {
            //Arrange
            var loginQuery = new LoginQuery();
            var result = new Result<LoginQueryResult>() { IsSuccessful = false };

            _mediatorMock.Setup(m => m.Send(It.IsAny<LoginQuery>(), default)).ReturnsAsync(result);

            //Act
            var loginAction = await _controller.Login(loginQuery);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(loginAction);
            Assert.Equal(result, badRequestResult.Value);
        }
    }
}
