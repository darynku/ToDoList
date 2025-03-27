using FluentValidation;
using MediatR;
using ToDoList.Services.Abstract;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.Create;

public class CreateNoteRequestHandler(INoteService noteService, IValidator<CreateNoteRequest> validator) : IRequestHandler<CreateNoteRequest>
{
    public async Task Handle(CreateNoteRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        
        await noteService.AddNoteAsync(request, cancellationToken);
    }
}