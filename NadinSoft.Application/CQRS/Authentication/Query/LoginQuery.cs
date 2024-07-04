using MediatR;
using NadinSoft.Application.Common;

namespace NadinSoft.Application.CQRS.Authentication.Query
{
    public sealed class LoginQuery : IRequest<Result<LoginQueryResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
