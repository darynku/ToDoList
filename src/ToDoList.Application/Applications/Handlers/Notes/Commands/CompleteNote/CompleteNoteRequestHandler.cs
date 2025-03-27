using FluentValidation;
using MediatR;
using ToDoList.Services.Abstract;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.CompleteNote;

public class CompleteNoteRequestHandler(INoteService noteService, IValidator<CompleteNoteRequest> validator) : IRequestHandler<CompleteNoteRequest>
{
    public async Task Handle(CompleteNoteRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        
        await noteService.CompleteNoteAsync(request.Id, cancellationToken);
    }
}