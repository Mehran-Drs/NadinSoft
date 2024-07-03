using MediatR;

namespace NadinSoft.Application.CQRS.Authentication.Query
{
    public sealed class LoginQuery : IRequest<LoginQueryResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
