using MediatR;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.CompleteNote;

public record CompleteNoteRequest(int Id) : IRequest;