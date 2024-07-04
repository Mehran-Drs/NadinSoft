using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Application.CQRS.Products.Commands.CreateProduct;
using NadinSoft.Application.CQRS.Products.Commands.DeleteProduct;
using NadinSoft.Application.CQRS.Products.Commands.EditProduct;
using NadinSoft.Application.CQRS.Products.Queries.GetProduct;
using NadinSoft.Application.CQRS.Products.Queries.GetProductsList;
using NadinSoft.Application.Services.Authentication.JwtServices;

namespace NadinSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator, IJwtService jwtService)
        {
            _mediator = mediator;
            _jwtService = jwtService;
        }

        [HttpGet("GetProductById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(int id)
        {
            var model = new GetProductQuery()
            {
                ProductId = id
            };

            var result = await _mediator.Send(model);

            if (result.IsSuccessful)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetProducts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts(int page, int limit, string? firstName = null , string? lastName = null)
        {
            var model = new GetProductsListQuery()
            {
                Limit = limit,
                FirstName = firstName,
                LastName = lastName,
                Page = page
            };

            var result = await _mediator.Send(model);

            if (result.IsSuccessful)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("CreateProduct")]
        [Authorize]
        public async Task<IActionResult> CreateProduct(CreateProductCommand model)
        {
            var idClaim = _jwtService.GetClaims(HttpContext.Request.Headers.Authorization.ToString()).FirstOrDefault(x => x.Type == "id").Value;
            model.CreatorId = Convert.ToInt32(idClaim);

            var result = await _mediator.Send(model);

            if (result.IsSuccessful)
            {
                return Created($"/api/products/getproductbyid/{result.Value}",result);
            }

            return BadRequest(result);
        }

        [HttpPut("EditProduct")]
        [Authorize]
        public async Task<IActionResult> EditProduct(EditProductCommand model)
        {
            var idClaim = _jwtService.GetClaims(HttpContext.Request.Headers.Authorization.ToString()).FirstOrDefault(x => x.Type == "id").Value;
            model.CreatorId = Convert.ToInt32(idClaim);

            var result = await _mediator.Send(model);

            if (result.IsSuccessful)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("DeleteProduct")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommand model)
        {
            var idClaim = _jwtService.GetClaims(HttpContext.Request.Headers.Authorization.ToString()).FirstOrDefault(x => x.Type == "id").Value;
            model.CreatorId = Convert.ToInt32(idClaim);

            var result = await _mediator.Send(model);

            if (result.IsSuccessful)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
