using Common.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreProducts.Core.Product.Command;
using StoreProducts.Core.Product.Entity;

namespace StoreProducts.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task<ServiceResponse<Product>> Create(CreateProductCommand input)
        {
            return _mediator.Send(input);
        } 
        
        [HttpPut]
        public Task<ServiceResponse<bool>> Update([FromQuery] UpdateProductCommand input)
        {
            return _mediator.Send(input);
        }
        
        [HttpDelete]
        public Task<ServiceResponse<bool>> Delete([FromQuery] DeleteProductCommand input)
        {
            return _mediator.Send(input);
        }
    }
}
