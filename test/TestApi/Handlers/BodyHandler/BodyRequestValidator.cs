using FluentValidation;

namespace PlumbR.TestApi.Handlers.BodyHandler
{
    public class BodyRequestValidator : AbstractValidator<BodyRequest>
    {
        public BodyRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}