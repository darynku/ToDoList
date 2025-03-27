using FluentValidation;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.Create;

public class CreateNoteRequestValidator : AbstractValidator<CreateNoteRequest>
{
    public CreateNoteRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Заголовок обязателен");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Описание обязателен");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Категория обязателена");
    }
}