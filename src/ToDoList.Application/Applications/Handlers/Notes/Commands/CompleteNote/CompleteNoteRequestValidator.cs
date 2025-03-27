using FluentValidation;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.CompleteNote;

public class CompleteNoteRequestValidator : AbstractValidator<CompleteNoteRequest>
{
    public CompleteNoteRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id не может быть пустым");
    }
}   