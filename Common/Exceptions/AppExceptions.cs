
using Common.ResultStatus;
using System.Net;

namespace Common.Exceptions;

public class AppExceptions : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }
    public ApiResultStatusCode ApiStatusCode { get; set; }
    public object AdditionalData { get; set; }


    public AppExceptions()
             : this(ApiResultStatusCode.ServerError)
    {
    }

    public AppExceptions(ApiResultStatusCode statusCode)
        : this(statusCode, null)
    {
    }

    public AppExceptions(string message)
        : this(ApiResultStatusCode.ServerError, message)
    {
    }

    public AppExceptions(ApiResultStatusCode statusCode, string message)
        : this(statusCode, message, HttpStatusCode.InternalServerError)
    {
    }

    public AppExceptions(string message, object additionalData)
        : this(ApiResultStatusCode.ServerError, message, additionalData)
    {
    }

    public AppExceptions(ApiResultStatusCode statusCode, object additionalData)
        : this(statusCode, null, additionalData)
    {
    }

    public AppExceptions(ApiResultStatusCode statusCode, string message, object additionalData)
        : this(statusCode, message, HttpStatusCode.InternalServerError, additionalData)
    {
    }

    public AppExceptions(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode)
        : this(statusCode, message, httpStatusCode, null)
    {
    }

    public AppExceptions(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
        : this(statusCode, message, httpStatusCode, null, additionalData)
    {
    }

    public AppExceptions(string message, Exception exception)
        : this(ApiResultStatusCode.ServerError, message, exception)
    {
    }

    public AppExceptions(string message, Exception exception, object additionalData)
        : this(ApiResultStatusCode.ServerError, message, exception, additionalData)
    {
    }

    public AppExceptions(ApiResultStatusCode statusCode, string message, Exception exception)
        : this(statusCode, message, HttpStatusCode.InternalServerError, exception)
    {
    }

    public AppExceptions(ApiResultStatusCode statusCode, string message, Exception exception, object additionalData)
        : this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
    {
    }

    public AppExceptions(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
        : this(statusCode, message, httpStatusCode, exception, null)
    {
    }

    public AppExceptions(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
        : base(message, exception)
    {
        ApiStatusCode = statusCode;
        HttpStatusCode = httpStatusCode;
        AdditionalData = additionalData;
    }
}
