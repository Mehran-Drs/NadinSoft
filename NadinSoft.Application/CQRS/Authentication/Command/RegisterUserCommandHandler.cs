using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NadinSoft.Application.Common;
using NadinSoft.Domain.Entities.Users;

namespace NadinSoft.Application.CQRS.Authentication.Command
{
    public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<int>>
    {
        
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public RegisterUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            Result<int> baseResult;
            var user = _mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                baseResult = new Result<int>(user.Id);
                return baseResult;
            }
            return baseResult = new Result<int>(0, false,result.Errors.Select(x=>new Error(x.Code,x.Description)).ToList());
        }
    }
}
