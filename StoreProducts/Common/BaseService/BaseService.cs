using Common.Response;
using System.Net;

namespace Common.BaseService
{
    public class BaseService
    {
        public ServiceResponse Success(int statusCode = (int)HttpStatusCode.OK, string message = nameof(HttpStatusCode.OK))
        {
            return ResponseHelper.Success(statusCode, message);
        }

        public ServiceResponse<T> Success<T>(T date, int statusCode = (int)HttpStatusCode.OK, string message = nameof(HttpStatusCode.OK))
        {
            return ResponseHelper.Success(date, statusCode, message);
        }
        
        public ServiceResponse<DataList<T>> Success<T>(DataList<T> date, int statusCode = (int)HttpStatusCode.OK, string message = nameof(HttpStatusCode.OK))
        {
            return ResponseHelper.Success(date, statusCode, message);
        }

        public ServiceResponse Invalid(int statusCode = (int)HttpStatusCode.BadRequest, string message = nameof(HttpStatusCode.BadRequest))
        {
            return ResponseHelper.Invalid(statusCode, message);
        }

        public ServiceResponse<T> Invalid<T>(T date, int statusCode = (int)HttpStatusCode.BadRequest, string message = nameof(HttpStatusCode.BadRequest))
        {
            return ResponseHelper.Invalid(date, statusCode, message);
        }

        public ServiceResponse NotFound(int statusCode = (int)HttpStatusCode.NotFound, string message = nameof(HttpStatusCode.NotFound))
        {
            return ResponseHelper.NotFound(statusCode, message);
        }

        public ServiceResponse<T> NotFound<T>(T date, int statusCode = (int)HttpStatusCode.NotFound, string message = nameof(HttpStatusCode.NotFound))
        {
            return ResponseHelper.NotFound(date, statusCode, message);
        }
    }
}
