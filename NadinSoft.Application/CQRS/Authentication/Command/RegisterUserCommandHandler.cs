using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NadinSoft.Domain.Entities.Users;

namespace NadinSoft.Application.CQRS.Authentication.Command
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly IValidator<RegisterUserCommand> _validator;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public RegisterUserCommandHandler(UserManager<User> userManager, IMapper mapper, IValidator<RegisterUserCommand> validator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken); 

            var user = _mapper.Map<User>(request.UserName);

            var result = await _userManager.CreateAsync(user, request.Password);

            return result;
        }
    }
}
