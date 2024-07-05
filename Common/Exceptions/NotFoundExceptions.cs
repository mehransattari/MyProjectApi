
using Common.ResultStatus;

namespace Common.Exceptions;

public class NotFoundExceptions : AppExceptions
{
    public NotFoundExceptions()
        : base(ApiResultStatusCode.NotFound)
    {
    }

    public NotFoundExceptions(string message)
        : base(ApiResultStatusCode.NotFound, message)
    {
    }

    public NotFoundExceptions(object additionalData)
        : base(ApiResultStatusCode.NotFound, additionalData)
    {
    }

    public NotFoundExceptions(string message, object additionalData)
        : base(ApiResultStatusCode.NotFound, message, additionalData)
    {
    }

    public NotFoundExceptions(string message, Exception exception)
        : base(ApiResultStatusCode.NotFound, message, exception)
    {
    }

    public NotFoundExceptions(string message, Exception exception, object additionalData)
        : base(ApiResultStatusCode.NotFound, message, exception, additionalData)
    {
    }
}
