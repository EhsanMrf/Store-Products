namespace Common.Response;

public class ServiceResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
}
public class ServiceResponse<T> : ServiceResponse
{
    public T Data { get; set; }
}