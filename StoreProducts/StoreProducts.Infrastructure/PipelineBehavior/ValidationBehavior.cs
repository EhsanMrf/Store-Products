using Common.Response;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace StoreProducts.Infrastructure.PipelineBehavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IServiceProvider _serviceProvider;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IServiceProvider serviceProvider)
        {
            _validators = validators;
            _serviceProvider = serviceProvider;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var validators = _serviceProvider.GetServices<IValidator<TRequest>>().ToArray();
            if (validators.Any())
            {
                var failures = validators
                    .Select(v => v.Validate(request))
                    .SelectMany(result => result.Errors)
                    .Where(error => error != null)
                    .ToList();

                if (failures.Any())
                {
                    var result = Activator.CreateInstance<TResponse>();
                    if (result is ServiceResponse response)
                    {
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response.Message = nameof(HttpStatusCode.BadRequest);
                        response.ServiceSubStatus = failures
                            .Select(error => new ServiceSubStatus
                            {
                                StatusCode = (int)HttpStatusCode.BadRequest,
                                Subject = error.PropertyName,
                                ErrorMessage = error.ErrorMessage
                            }).ToList();
                    }
                    return result;
                }
            }
            return await next();
        }
    }
}
