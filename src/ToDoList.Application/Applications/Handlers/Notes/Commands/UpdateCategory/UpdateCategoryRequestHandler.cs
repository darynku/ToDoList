using FluentValidation;
using MediatR;
using ToDoList.Services.Abstract;

namespace ToDoList.Application.Applications.Handlers.Notes.Commands.UpdateCategory;

public class UpdateCategoryRequestHandler(INoteService noteService, IValidator<UpdateCategoryRequest> validator) : IRequestHandler<UpdateCategoryRequest>
{
    public async Task Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        
        await noteService.UpdateCategoryAsync(request, cancellationToken);
    }
}