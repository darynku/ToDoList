using ToDoList.Application.Applications.Handlers.Notes.Commands.Create;
using ToDoList.Application.Applications.Handlers.Notes.Commands.Update;
using ToDoList.Application.Applications.Handlers.Notes.Commands.UpdateCategory;
using ToDoList.Application.Applications.Handlers.Notes.Commands.UpdateDueDate;
using ToDoList.Domain;

namespace ToDoList.Services.Abstract;

public interface INoteService
{
    Task<IEnumerable<Note>> GetAllNotesAsync();
    Task<Note> GetNoteByIdAsync(int id, CancellationToken cancellationToken);
    Task AddNoteAsync(CreateNoteRequest request, CancellationToken cancellationToken);
    Task UpdateNoteAsync(UpdateNoteRequest request, CancellationToken cancellationToken);
    Task CompleteNoteAsync(int id, CancellationToken cancellationToken);
    Task UpdateDueDateAsync(UpdateDueDateRequest request, CancellationToken cancellationToken);
    Task UpdateCategoryAsync(UpdateCategoryRequest request, CancellationToken cancellationToken);
    Task DeleteNoteAsync(int id, CancellationToken cancellationToken);
}