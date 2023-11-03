using Common.Response;
using MediatR;
using MediatR.Pipeline;

namespace StoreProducts.Infrastructure.MediatR
{
    public class ManageExceptionBehavior<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TException : Exception where TRequest : IRequest<TResponse> where TResponse : ServiceResponse, new()
    {

        public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {

            var response = new TResponse
            {
                ServiceException = new ServiceException
                {
                    ErrorMessage = exception.Message,
                    StatusCode = 501
                }
            };

            state.SetHandled(response);
            return Task.CompletedTask;
        }
    }
}