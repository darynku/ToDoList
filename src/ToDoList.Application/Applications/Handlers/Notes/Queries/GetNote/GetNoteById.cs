using MediatR;
using ToDoList.Application.Conracts.Response;

namespace ToDoList.Application.Applications.Handlers.Notes.Queries.GetNote;

public record GetNoteById(int Id) : IRequest<NoteResponse>;