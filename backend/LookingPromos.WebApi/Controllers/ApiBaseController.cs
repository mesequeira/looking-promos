using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LookingPromos.Controllers;

/// <summary>
/// A base class for API controllers, providing common functionality and properties.
/// It inherits from ControllerBase, the base class for Web API controllers.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApiBaseController : ControllerBase
{
    /// <summary>
    /// The mediator instance for handling MediatR requests.
    /// </summary>
    private ISender _sender = null!;

    /// <summary>
    /// A read-only property to get the Mediator instance, ensuring it is initialized when accessed.
    /// </summary>
    protected ISender Sender =>
        _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
