using System.Net;

namespace Common.Response;

public class ResponseHelper
{
    public static ServiceResponse Success(int statusCode, string message) => Response(statusCode, message);

    public static ServiceResponse<T> Success<T>(T date, int statusCode, string message) => Response(date, statusCode, message);

    public static ServiceResponse Invalid(int statusCode, string message) => Response(statusCode, message);

    public static ServiceResponse<T> Invalid<T>(T data, int statusCode = (int)HttpStatusCode.BadRequest, string message = nameof(HttpStatusCode.BadRequest)) => Response(data, statusCode, message);

    public static ServiceResponse NotFound(int statusCode = (int)HttpStatusCode.NotFound, string message = nameof(HttpStatusCode.NotFound)) => Response(statusCode, message);

    public static ServiceResponse<T> NotFound<T>(T data, int statusCode = (int)HttpStatusCode.NotFound, string message = nameof(HttpStatusCode.NotFound)) => Response(data, statusCode, message);


    public static ServiceResponse Response(int statusCode, string message)
    {
        return new ServiceResponse
        {
            Message = message,
            StatusCode = statusCode,
        };
    }

    /// <summary>
    /// This method is mostly used in large projects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="statusCode"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ServiceResponse<T> Response<T>(T data, int statusCode, string message)
    {
        return new ServiceResponse<T>
        {
            Data = data,
            Message = message,
            StatusCode = statusCode
        };
    }

}