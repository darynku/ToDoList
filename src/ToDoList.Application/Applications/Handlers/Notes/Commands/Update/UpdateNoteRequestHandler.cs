using FluentValidation;
using MediatR;
using ToDoList.Services.Abstract;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.Update;

public class UpdateNoteRequestHandler(INoteService noteService, IValidator<UpdateNoteRequest> validator) : IRequestHandler<UpdateNoteRequest>
{
    public async Task Handle(UpdateNoteRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        
        await noteService.UpdateNoteAsync(request, cancellationToken);
    }
}