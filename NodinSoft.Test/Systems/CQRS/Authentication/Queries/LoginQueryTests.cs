using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NadinSoft.Application.Common;
using NadinSoft.Application.CQRS.Authentication.Query;
using NadinSoft.Application.Services.Authentication.JwtServices;
using NadinSoft.Domain.Entities.Users;
using System.Security.Claims;

namespace NodinSoft.Test.Systems.CQRS.Authentication.Queries
{
    public class LoginQueryTests
    {
        [Fact]
        public async Task Login_ShouldReturnSuccessfulResult_WhenLoginSucceeded()
        {
            // Arrange
            var loginQuery = new LoginQuery();

            var result = new Result<LoginQueryResult> { IsSuccessful = true };

            // Mocks for UserManager<TUser> constructor parameters
            var userStoreMock = new Mock<IUserStore<User>>();
            var optionsMock = new Mock<IOptions<IdentityOptions>>();
            var passwordHasherMock = new Mock<IPasswordHasher<User>>();
            var userValidators = new List<IUserValidator<User>> { new Mock<IUserValidator<User>>().Object };
            var passwordValidators = new List<IPasswordValidator<User>> { new Mock<IPasswordValidator<User>>().Object };
            var keyNormalizerMock = new Mock<ILookupNormalizer>();
            var errorsMock = new Mock<IdentityErrorDescriber>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = new Mock<ILogger<UserManager<User>>>();

            var userManagerMock = new Mock<UserManager<User>>(
                userStoreMock.Object,
                optionsMock.Object,
                passwordHasherMock.Object,
                userValidators,
                passwordValidators,
                keyNormalizerMock.Object,
                errorsMock.Object,
                serviceProviderMock.Object,
                loggerMock.Object);

            var jwtMock = new Mock<IJwtService>();

            var user = new User ();

            userManagerMock.Setup(u => u.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            userManagerMock.Setup(u => u.CheckPasswordAsync(It.IsAny<User>(), loginQuery.Password)).ReturnsAsync(true);
            jwtMock.Setup(j => j.GenerateToken(It.IsAny<List<Claim>>())).Returns("Bearer");

            var handler = new LoginQueryHandler(jwtMock.Object, userManagerMock.Object);

            // Act
            var handlerResult = await handler.Handle(loginQuery, CancellationToken.None);

            // Assert
            Assert.True(handlerResult.IsSuccessful);
            Assert.Equal("Bearer", handlerResult.Value.Token);
            Assert.Null(handlerResult.Errors);
        }

        [Fact]
        public async Task Login_ShouldReturnUnsuccessfulResult_WhenLoginFailed()
        {
            // Arrange
            var loginQuery = new LoginQuery();

            var result = new Result<LoginQueryResult> 
            {
                IsSuccessful = false ,
                Errors = new List<Error> { new Error("NOTFOUND", "User Not Found") }
            };

            // Mocks for UserManager<TUser> constructor parameters
            var userStoreMock = new Mock<IUserStore<User>>();
            var optionsMock = new Mock<IOptions<IdentityOptions>>();
            var passwordHasherMock = new Mock<IPasswordHasher<User>>();
            var userValidators = new List<IUserValidator<User>> { new Mock<IUserValidator<User>>().Object };
            var passwordValidators = new List<IPasswordValidator<User>> { new Mock<IPasswordValidator<User>>().Object };
            var keyNormalizerMock = new Mock<ILookupNormalizer>();
            var errorsMock = new Mock<IdentityErrorDescriber>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var loggerMock = new Mock<ILogger<UserManager<User>>>();

            var userManagerMock = new Mock<UserManager<User>>(
                userStoreMock.Object,
                optionsMock.Object,
                passwordHasherMock.Object,
                userValidators,
                passwordValidators,
                keyNormalizerMock.Object,
                errorsMock.Object,
                serviceProviderMock.Object,
                loggerMock.Object);

            var jwtMock = new Mock<IJwtService>();

            userManagerMock.Setup(u => u.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((User)null);
            userManagerMock.Setup(u => u.CheckPasswordAsync(It.IsAny<User>(), loginQuery.Password)).ReturnsAsync(true);
            jwtMock.Setup(j => j.GenerateToken(It.IsAny<List<Claim>>())).Returns("Bearer");

            var handler = new LoginQueryHandler(jwtMock.Object, userManagerMock.Object);

            // Act
            var handlerResult = await handler.Handle(loginQuery, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.NotNull(result.Errors);
            Assert.Equal(handlerResult.Errors.First().ErrorCode, result.Errors.First().ErrorCode);
            Assert.Equal(handlerResult.Errors.First().ErrorMessage, result.Errors.First().ErrorMessage);
        }
    }
}
