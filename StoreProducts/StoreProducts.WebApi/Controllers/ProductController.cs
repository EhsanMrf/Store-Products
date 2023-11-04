using Common.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreProducts.Core.Product.Command;
using StoreProducts.Core.Product.Entity;
using StoreProducts.Core.Product.Query;
using StoreProducts.Core.Product.Query.Result;

namespace StoreProducts.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public Task<ServiceResponse<Product>> Create(CreateProductCommand input)
        {
            return _mediator.Send(input);
        } 
        
        [HttpPut]
        [Authorize]
        public Task<ServiceResponse<bool>> Update([FromQuery] UpdateProductCommand input)
        {
            return _mediator.Send(input);
        }
        
        [HttpDelete]
        [Authorize]
        public Task<ServiceResponse<bool>> Delete([FromQuery] DeleteProductCommand input)
        {
            return _mediator.Send(input);
        } 
        
        [HttpGet]
        public Task<ServiceResponse<DataList<ProductResult>>> GetAll([FromQuery] ProductQuery input)
        {
            return _mediator.Send(input);
        }
    }
}
