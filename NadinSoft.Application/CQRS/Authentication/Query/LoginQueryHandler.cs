using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NadinSoft.Application.Common;
using NadinSoft.Application.Services.Authentication.JwtServices;
using NadinSoft.Domain.Entities.Users;
using System.Net;
using System.Security.Claims;

namespace NadinSoft.Application.CQRS.Authentication.Query
{
    public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, Result<LoginQueryResult>>
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;

        public LoginQueryHandler(IJwtService jwtService, UserManager<User> userManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<Result<LoginQueryResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            Result<LoginQueryResult> baseResult;

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
                    baseResult = new Result<LoginQueryResult>(result);
                    return baseResult;
                }
            }

            baseResult = new Result<LoginQueryResult>(result, false, new List<Error>() { new Error("NOTFOUND","User Not Found")});
            return baseResult;
        }
    }
}
