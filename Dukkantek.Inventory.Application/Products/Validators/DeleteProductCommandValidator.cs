using Dukkantek.Inventory.Application.Products.Commands;
using FluentValidation;

namespace Dukkantek.Inventory.Application.Products.Validators
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
        }
    }
}
