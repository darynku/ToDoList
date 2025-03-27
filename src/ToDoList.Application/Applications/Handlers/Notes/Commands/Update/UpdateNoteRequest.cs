using MediatR;
using ToDoList.Application.Applications.Handlers.Notes.Commands.Create;
using ToDoList.Application.Conracts.Requests;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.Update;

public record UpdateNoteRequest(int Id, NoteData Data) : IRequest;