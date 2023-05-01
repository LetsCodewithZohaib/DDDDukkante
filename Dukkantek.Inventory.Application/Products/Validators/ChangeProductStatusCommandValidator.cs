using Dukkantek.Inventory.Application.Products.Commands;
using FluentValidation;

namespace Dukkantek.Inventory.Application.Products.Validators
{
    public class ChangeProductStatusCommandValidator : AbstractValidator<ChangeProductStatusCommand>
    {
        public ChangeProductStatusCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
            RuleFor(e => e.Status).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
        }
    }
}
