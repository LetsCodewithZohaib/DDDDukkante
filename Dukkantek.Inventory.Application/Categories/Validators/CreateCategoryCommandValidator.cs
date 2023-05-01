using Dukkantek.Inventory.Application.Categories.Commands;
using FluentValidation;

namespace Dukkantek.Inventory.Application.Categories.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
     {
          public CreateCategoryCommandValidator()
          {
               RuleFor(e => e.Name).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
          }
     }
}