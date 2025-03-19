using ToDoList.Contracts;
using ToDoList.Domain;

namespace ToDoList.Services.Abstract;

public interface INoteService
{
    Task<IEnumerable<Note>> GetAllNotesAsync();
    Task<Note> GetNoteByIdAsync(int id, CancellationToken cancellationToken);
    Task AddNoteAsync(CreateNoteRequest request, CancellationToken cancellationToken);
    Task UpdateNoteAsync(int id, UpdateNoteRequest request, CancellationToken cancellationToken);
    Task CompleteNoteAsync(int id, CancellationToken cancellationToken);
    Task UpdateDueDateAsync(int id, UpdateDueDateRequest request, CancellationToken cancellationToken);
    Task UpdateCategoryAsync(int id, UpdateCategoryRequest request, CancellationToken cancellationToken);
    Task DeleteNoteAsync(int id, CancellationToken cancellationToken);
}