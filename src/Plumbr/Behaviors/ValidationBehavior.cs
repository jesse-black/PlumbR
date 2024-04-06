using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace PlumbR.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, PipelineResult<TResponse>>
        where TRequest : IRequest<PipelineResult<TResponse>>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<PipelineResult<TResponse>> Handle(
            TRequest request,
            RequestHandlerDelegate<PipelineResult<TResponse>> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var results = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context)));
            var result = new ValidationResult(results);

            if (!result.IsValid)
            {
                return result;
            }

            return await next();
        }
    }
}