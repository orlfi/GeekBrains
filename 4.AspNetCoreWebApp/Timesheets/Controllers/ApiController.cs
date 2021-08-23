using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Timesheets.Controllers
{
    public abstract class ApiController: ControllerBase
    {
        private IMediator _mediator;
 
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;
    }
}