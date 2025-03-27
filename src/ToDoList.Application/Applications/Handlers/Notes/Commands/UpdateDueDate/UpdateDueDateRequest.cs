using MediatR;
using ToDoList.Application.Applications.Handlers.Notes.Commands.Update;
using ToDoList.Application.Conracts.Requests;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.UpdateDueDate;

public record UpdateDueDateRequest(int Id, NoteDueDateData Data) : IRequest;