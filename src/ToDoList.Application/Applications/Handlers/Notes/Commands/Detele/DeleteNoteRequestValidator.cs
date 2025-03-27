using FluentValidation;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.Detele;

public class DeleteNoteRequestValidator : AbstractValidator<DeleteNoteRequest>
{
    public DeleteNoteRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Id не может быть пустым");
    }
}