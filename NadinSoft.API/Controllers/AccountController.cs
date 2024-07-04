using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Application.CQRS.Authentication.Command;
using NadinSoft.Application.CQRS.Authentication.Query;

namespace NadinSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserCommand model)
        {
            var result = await _mediator.Send(model);

            if (result.IsSuccessful)
            {
                return Created("/", result);
            }

            return BadRequest(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginQuery model)
        {
            var result = await _mediator.Send(model);

            if (result.IsSuccessful)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
