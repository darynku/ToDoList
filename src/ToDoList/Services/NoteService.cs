using Microsoft.EntityFrameworkCore;
using ToDoList.Contracts;
using ToDoList.Domain;
using ToDoList.Services.Abstract;

namespace ToDoList.Services;

public class NoteService(ApplicationDbContext context) : INoteService
{
    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await context.Notes
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<Note> GetNoteByIdAsync(int id, CancellationToken cancellationToken)
    {
        var note = await context.Notes
            .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);

        if (note is null)
            throw new Exception("Note not found");
        
        return note;
    }

    public async Task AddNoteAsync(CreateNoteRequest request, CancellationToken cancellationToken)
    {
        var note = new Note(request.Title, request.Description, request.Category);
        await context.Notes.AddAsync(note, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateNoteAsync(int id, UpdateNoteRequest request, CancellationToken cancellationToken)
    {
        var updateNote = await context.Notes
            .Where(n => n.Id == id)
            .ExecuteUpdateAsync(setters => setters
                    .SetProperty(n => n.Title, request.Title)
                    .SetProperty(n => n.Description, request.Description)
                    .SetProperty(n => n.IsCompleted, request.IsCompleted),
                cancellationToken);

        if (updateNote == 0)
            throw new Exception("Note not found");

    }

    public async Task CompleteNoteAsync(int id, CancellationToken cancellationToken)
    {
        var note = await context.Notes.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        
        if (note is null)
            throw new Exception("Note not found");

        note.IsCompleted = true;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateDueDateAsync(int id,UpdateDueDateRequest request, CancellationToken cancellationToken)
    {
        var note = await context.Notes.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        
        if (note is null) 
            throw new Exception("Note not found");

        note.DueDate = request.NewDueDate;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCategoryAsync(int id, UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var note = await context.Notes.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        
        if (note is null) 
            throw new Exception("Note not found");

        note.Category = request.Category;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteNoteAsync(int id, CancellationToken cancellationToken)
    {
        var note = await context.Notes.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        
        if (note is null)
            throw new Exception("Note not found");

        context.Notes.Remove(note);
        await context.SaveChangesAsync(cancellationToken);
    }
}