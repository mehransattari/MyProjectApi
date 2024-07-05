
using Common.ResultStatus;

namespace Common.Exceptions;

public class BadRequestExceptions : AppExceptions
{
    public BadRequestExceptions()
             : base(ApiResultStatusCode.BadRequest)
    {
    }

    public BadRequestExceptions(string message)
        : base(ApiResultStatusCode.BadRequest, message)
    {
    }

    public BadRequestExceptions(object additionalData)
        : base(ApiResultStatusCode.BadRequest, additionalData)
    {
    }

    public BadRequestExceptions(string message, object additionalData)
        : base(ApiResultStatusCode.BadRequest, message, additionalData)
    {
    }

    public BadRequestExceptions(string message, Exception exception)
        : base(ApiResultStatusCode.BadRequest, message, exception)
    {
    }

    public BadRequestExceptions(string message, Exception exception, object additionalData)
        : base(ApiResultStatusCode.BadRequest, message, exception, additionalData)
    {
    }
}
