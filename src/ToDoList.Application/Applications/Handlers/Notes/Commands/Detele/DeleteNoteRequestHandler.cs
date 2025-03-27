using FluentValidation;
using MediatR;
using ToDoList.Services.Abstract;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.Detele;

public class DeleteNoteRequestHandler(INoteService noteService, IValidator<DeleteNoteRequest> validator) : IRequestHandler<DeleteNoteRequest>
{
    public async Task Handle(DeleteNoteRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        
        await noteService.DeleteNoteAsync(request.Id, cancellationToken);
    }
}