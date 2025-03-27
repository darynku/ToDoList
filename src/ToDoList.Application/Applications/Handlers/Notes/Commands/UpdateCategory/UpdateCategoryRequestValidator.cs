using FluentValidation;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.UpdateCategory;

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Data.Category).NotEmpty().WithMessage("Категория обязательна");
    }
}