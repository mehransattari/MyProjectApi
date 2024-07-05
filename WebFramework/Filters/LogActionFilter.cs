using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;


namespace WebFramework.Filters;

public class LogActionFilter : IActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // وقتی قبل از اجرای اکشن فراخوانده می‌شود
        _logger.LogInformation("Executing action {action} with arguments {arguments}",
            context.ActionDescriptor.DisplayName, context.ActionArguments);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // وقتی بعد از اجرای اکشن فراخوانده می‌شود
        _logger.LogInformation("Executed action {action} with result {result}",
            context.ActionDescriptor.DisplayName, context.Result);
    }
}
