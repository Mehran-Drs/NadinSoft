using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NadinSoft.Application.Common;
using NadinSoft.Application.CQRS.Authentication.Command;
using NadinSoft.Domain.Entities.Users;

namespace NodinSoft.Test.Systems.CQRS.Authentication.Commands
{
    public class RegisterUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnSuccessfulResult_WhenUserCreationSucceeds()
        {
            // Arrange
            RegisterUserCommand command = new RegisterUserCommand();
            var result = new Result<int>() { Value = 1, IsSuccessful = true };
            //
            // Mocks for UserManager<TUser> constructor parameters
            var userStoreMock = new Mock<IUserStore<User>>();
            var optionsMock = new Mock<IOptions<IdentityOptions>>();
            var passwordHasherMock = new Mock<IPasswordHasher<User>>();
            var userValidators = new List<Mock<IUserValidator<User>>>();
            var passwordValidators = new List<Mock<IPasswordValidator<User>>>();
            var keyNormalizerMock = new Mock<ILookupNormalizer>();
            var errorsMock = new Mock<IdentityErrorDescriber>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = new Mock<ILogger<UserManager<User>>>();

            var userManagerMock = new Mock<UserManager<User>>(
                    userStoreMock.Object,
                    optionsMock.Object,
                    passwordHasherMock.Object,
                    userValidators.Select(v => v.Object),
                    passwordValidators.Select(v => v.Object),
                    keyNormalizerMock.Object,
                    errorsMock.Object,
                    serviceProviderMock.Object,
                    loggerMock.Object);

            var mapperMock = new Mock<IMapper>();
            var user = new User();
            var identityResult = new IdentityResult();

            mapperMock.Setup(m => m.Map<User>(It.IsAny<RegisterUserCommand>())).Returns(user);
            userManagerMock.Setup(u => u.CreateAsync(It.IsAny<User>(), command.Password)).ReturnsAsync(IdentityResult.Success);

            var handler = new RegisterUserCommandHandler(userManagerMock.Object, mapperMock.Object);

            //Act
            var handle = await handler.Handle(command, CancellationToken.None);
            //Assert
            Assert.Equal(handle.IsSuccessful, result.IsSuccessful);
            Assert.Null(handle.Errors);
        }

        [Fact]
        public async Task Handle_ShouldReturnUnsuccessfulResult_WhenUserCreationFails()
        {
            // Arrange
            RegisterUserCommand command = new RegisterUserCommand();
            var result = new Result<int>() { Value = 1, IsSuccessful = false };
            // Mocks for UserManager<TUser> constructor parameters
            var userStoreMock = new Mock<IUserStore<User>>();
            var optionsMock = new Mock<IOptions<IdentityOptions>>();
            var passwordHasherMock = new Mock<IPasswordHasher<User>>();
            var userValidators = new List<Mock<IUserValidator<User>>>();
            var passwordValidators = new List<Mock<IPasswordValidator<User>>>();
            var keyNormalizerMock = new Mock<ILookupNormalizer>();
            var errorsMock = new Mock<IdentityErrorDescriber>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = new Mock<ILogger<UserManager<User>>>();

            var userManagerMock = new Mock<UserManager<User>>(
                    userStoreMock.Object,
                    optionsMock.Object,
                    passwordHasherMock.Object,
                    userValidators.Select(v => v.Object),
                    passwordValidators.Select(v => v.Object),
                    keyNormalizerMock.Object,
                    errorsMock.Object,
                    serviceProviderMock.Object,
                    loggerMock.Object);

            var mapperMock = new Mock<IMapper>();
            var user = new User();
            var identityResult = new IdentityResult();

            mapperMock.Setup(m => m.Map<User>(It.IsAny<RegisterUserCommand>())).Returns(user);
            userManagerMock.Setup(u => u.CreateAsync(It.IsAny<User>(), command.Password)).ReturnsAsync(IdentityResult.Failed());

            var handler = new RegisterUserCommandHandler(userManagerMock.Object, mapperMock.Object);

            //Act
            var handle = await handler.Handle(command, CancellationToken.None);
            //Assert
            Assert.False(handle.IsSuccessful);
            Assert.Equal(handle.IsSuccessful, result.IsSuccessful);
            Assert.NotNull(handle.Errors);
        }
    }
}
