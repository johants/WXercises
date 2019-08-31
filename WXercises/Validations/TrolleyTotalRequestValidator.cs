using FluentValidation;
using WXercises.Models;

namespace WXercises.Validations
{
    public class TrolleyTotalRequestValidator : AbstractValidator<TrolleyTotalRequest>
    {
        public TrolleyTotalRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(req => req.Products)
                .NotNull();

            RuleFor(req => req.Specials)
                .NotNull();

            RuleFor(req => req.Quantities)
                .NotNull();
        }
    }
}
