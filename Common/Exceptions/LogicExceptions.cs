
using Common.ResultStatus;

namespace Common.Exceptions;

public class LogicExceptions : AppExceptions
{
    public LogicExceptions()
        : base(ApiResultStatusCode.LogicError)
    {
    }

    public LogicExceptions(string message)
        : base(ApiResultStatusCode.LogicError, message)
    {
    }

    public LogicExceptions(object additionalData)
        : base(ApiResultStatusCode.LogicError, additionalData)
    {
    }

    public LogicExceptions(string message, object additionalData)
        : base(ApiResultStatusCode.LogicError, message, additionalData)
    {
    }

    public LogicExceptions(string message, Exception exception)
        : base(ApiResultStatusCode.LogicError, message, exception)
    {
    }

    public LogicExceptions(string message, Exception exception, object additionalData)
        : base(ApiResultStatusCode.LogicError, message, exception, additionalData)
    {
    }
}