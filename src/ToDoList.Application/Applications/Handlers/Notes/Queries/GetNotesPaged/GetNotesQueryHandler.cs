using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Common.Paged;
using ToDoList.Application.Conracts.Response;
using ToDoList.Infrastructure;

namespace ToDoList.Application.Applications.Handlers.Notes.Queries.GetNotesPaged;

public class GetNotesQueryHandler(ApplicationDbContext context) : IRequestHandler<GetNotesQuery, PagedList<NotePagedListItem>>
{
    public async Task<PagedList<NotePagedListItem>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        var query = await context.Notes
            .Select(n => new NotePagedListItem
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                IsCompleted = n.IsCompleted,
                Created = n.Created,
                DueDate = n.DueDate,
                Category = n.Category
            }).ToPagedList(request.Page, request.PageSize, cancellationToken);

        return query;
    }
}