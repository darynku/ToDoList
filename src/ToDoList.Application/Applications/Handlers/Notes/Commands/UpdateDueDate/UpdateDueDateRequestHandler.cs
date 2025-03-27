using MediatR;
using ToDoList.Services.Abstract;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.UpdateDueDate;

public class UpdateDueDateRequestHandler(INoteService noteService) : IRequestHandler<UpdateDueDateRequest>
{
    public async Task Handle(UpdateDueDateRequest request, CancellationToken cancellationToken)
    {
        await noteService.UpdateDueDateAsync(request, cancellationToken);
    }
}