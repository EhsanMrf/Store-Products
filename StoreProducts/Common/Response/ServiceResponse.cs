using System.Net;

namespace Common.Response;

public class ServiceResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public List<ServiceSubStatus> ServiceSubStatus { get; set; }
    public ServiceException ServiceException { get; set; }
}
public class ServiceResponse<T> : ServiceResponse
{
    public T? Data { get; set; }

    public static implicit operator ServiceResponse<T>(T data)
    {
        if (data is null)
        {
            return new ServiceResponse<T>
            {
                Data = default,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Ops!"
            };
        }
        return new ServiceResponse<T>
        {
            Data = data,
            StatusCode = (int)HttpStatusCode.OK,
            
        };
    }
}