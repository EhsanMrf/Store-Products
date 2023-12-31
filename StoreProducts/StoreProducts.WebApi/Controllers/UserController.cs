﻿using Common.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreProducts.Core.User.Command;
using StoreProducts.Core.User.Query;

namespace StoreProducts.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) => _mediator = mediator;

        [HttpPost("RegisterUser")]
        public Task<ServiceResponse<bool>> RegisterUser([FromQuery] UserRegisterCommand input) => _mediator.Send(input);


        [HttpGet]
        public Task<ServiceResponse<string>> Login([FromQuery] UserLoginQuery input) => _mediator.Send(input);

    }
}
