using MediatR;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.Detele;

public record DeleteNoteRequest(int Id) : IRequest;