﻿using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CharityAuction.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??=
            HttpContext.RequestServices.GetService<IMediator>()!;

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                if (result is null)
                {
                    return Ok(result.Value);
                }

                return (result.Value is null) ?
                    NotFound("Found result matching null") : Ok(result.Value);
            }

            return BadRequest(result.Reasons);
        }
    }
}