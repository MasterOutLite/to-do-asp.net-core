using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Controllers.Base;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private ISender _sender;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
}