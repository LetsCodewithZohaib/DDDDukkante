using FluentValidation;
using Dukkantek.Inventory.Application.Categories.Commands;

namespace Dukkantek.Inventory.Application.Categories.Validators
{
     public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
     {
          public UpdateCategoryCommandValidator()
          {
               RuleFor(e => e.Id).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
               RuleFor(e => e.Name).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
          }
     }
}