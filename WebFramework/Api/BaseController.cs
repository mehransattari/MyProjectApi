using Microsoft.AspNetCore.Mvc;
using WebFramework.Filters;

namespace WebFramework.Api;

[ApiController]
[Route("api/[controller]")]
[ApiResultFilter]
[ServiceFilter(typeof(LogActionFilter))]
public class BaseController : ControllerBase
{

}
