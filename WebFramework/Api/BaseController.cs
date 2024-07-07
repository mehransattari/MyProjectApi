using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Filters;

namespace WebFramework.Api;

[ApiController]
[Route("api/[controller]")]
[ApiResultFilter]
[ServiceFilter(typeof(LogActionFilter))]
//[Authorize(Roles = "Admin")]
public class BaseController : ControllerBase
{

}
