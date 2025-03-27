using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Common.Exceptions;
using ToDoList.Application.Conracts.Response;
using ToDoList.Infrastructure;

namespace ToDoList.Application.Applications.Handlers.Notes.Queries.GetNote;

public class GetNoteByIdHandler(ApplicationDbContext context) : IRequestHandler<GetNoteById, NoteResponse>
{
    public async Task<NoteResponse> Handle(GetNoteById request, CancellationToken cancellationToken)
    {
        var note = await context.Notes
            .Where(n => n.Id == request.Id)
            .Select(n => new NoteResponse
        {
            Id = n.Id,
            Title = n.Title,
            Description = n.Description,
            IsCompleted = n.IsCompleted,
            DueDate = n.DueDate,
            Category = n.Category
        }).FirstOrDefaultAsync(cancellationToken);

        if (note is null)
            throw new NotFoundException("Такая задача не найдена");
        
        return note;
    }
}