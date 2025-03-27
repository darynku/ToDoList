using MediatR;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.Create;

public record CreateNoteRequest(string Title, string Description, string Category) : IRequest;