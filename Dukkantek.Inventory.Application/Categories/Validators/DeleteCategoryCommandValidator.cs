using FluentValidation;
using Dukkantek.Inventory.Application.Categories.Commands;

namespace Dukkantek.Inventory.Application.Categories.Validators
{
     public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
     {
          public DeleteCategoryCommandValidator()
          {
               RuleFor(e => e.Id).NotEmpty().NotNull().WithMessage("{PropertyName} cannot be null or empty");
          }
     }
}