using MediatR;
using ToDoList.Application.Common.Paged;
using ToDoList.Application.Conracts.Response;

namespace ToDoList.Application.Applications.Handlers.Notes.Queries.GetNotesPaged;

public record GetNotesQuery(int Page, int PageSize) : IRequest<PagedList<NotePagedListItem>>;