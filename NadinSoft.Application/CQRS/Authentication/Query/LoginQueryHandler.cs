using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NadinSoft.Application.Services.Authentication;
using NadinSoft.Domain.Entities.Users;
using System.Security.Claims;

namespace NadinSoft.Application.CQRS.Authentication.Query
{
    internal sealed class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResult>
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;

        public LoginQueryHandler(IJwtService jwtService, UserManager<User> userManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<LoginQueryResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            var result = new LoginQueryResult();

            if (user != null)
            {
                var isCorrect = await _userManager.CheckPasswordAsync(user, request.Password);

                if (isCorrect)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim("id",user.Id.ToString())
                    };
                    var jwt = _jwtService.GenerateToken(claims);
                    result.Token = jwt;
                    return result;
                }
            }

            throw new ValidationException(
                new List<ValidationFailure>()
                {
                    new ValidationFailure()
                        {
                            ErrorCode = "NotFound",
                            ErrorMessage = "User Not Found"
                        }
                });
        }
    }
}
