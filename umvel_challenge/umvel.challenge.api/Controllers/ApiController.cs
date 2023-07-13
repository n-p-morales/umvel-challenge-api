using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using umvel.challenge.application.Exceptions;

namespace umvel.challenge.api.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly IMediator mediator;

        protected IMediator ApiMediator => this.mediator ?? this.HttpContext.RequestServices.GetService<IMediator>();

        public IActionResult Success(object response = null)
        {
            return this.Ok(HttpApiResponse.Ok(ResponseCode.Success, response));
        }

        public IActionResult InernalServerError(object response = null)
        {
            return this.StatusCode(500, HttpApiResponse.InternalServerError(ResponseCode.InternalError, response));
        }

        public IActionResult InvalidRequest(object response = null)
        {
            return this.BadRequest(HttpApiResponse.BadRequest(ResponseCode.ValidationError, response));
        }
    }
}
