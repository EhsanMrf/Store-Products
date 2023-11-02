using FluentValidation;
using MediatR;

namespace StoreProducts.Infrastructure.PipelineBehavior
{
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();
            



            throw new NotImplementedException();
        }
    }
}
