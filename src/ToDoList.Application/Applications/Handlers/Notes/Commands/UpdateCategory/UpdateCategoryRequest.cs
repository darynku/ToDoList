using MediatR;
using ToDoList.Application.Applications.Handlers.Notes.Commands.Update;
using ToDoList.Application.Conracts.Requests;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.UpdateCategory;

public record UpdateCategoryRequest(int Id, NoteCategoryData Data) : IRequest;