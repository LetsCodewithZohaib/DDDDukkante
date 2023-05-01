using Dukkantek.Inventory.Application.Products.Commands;
using FluentValidation;

namespace Dukkantek.Inventory.Application.Products.Validators
{
    public class SellProductCommandValidator : AbstractValidator<SellProductCommand>
    {
        public SellProductCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
        }
    }
}
