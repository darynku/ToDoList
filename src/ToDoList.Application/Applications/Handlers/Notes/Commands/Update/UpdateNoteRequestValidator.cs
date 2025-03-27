using FluentValidation;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.Update;

public class UpdateNoteRequestValidator : AbstractValidator<UpdateNoteRequest>
{
    public UpdateNoteRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id обязательно");
        RuleFor(x => x.Data.Title).NotEmpty().WithMessage("Заголовок обязателен");
        RuleFor(x => x.Data.Description).NotEmpty().WithMessage("Описание обязательно");
    }
}