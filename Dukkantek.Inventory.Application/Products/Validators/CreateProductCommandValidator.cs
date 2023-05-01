using Dukkantek.Inventory.Application.Products.Commands;
using FluentValidation;

namespace Dukkantek.Inventory.Application.Products.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(e => e.Name).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
            RuleFor(e => e.Status).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
            RuleFor(e => e.BarCode).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
            RuleFor(e => e.Description).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
            RuleFor(e => e.Weight).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
            RuleFor(e => e.CategoryId).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
        }
    }
}
