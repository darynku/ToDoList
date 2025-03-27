using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Applications.Handlers.Notes.Commands.Create;
using ToDoList.Application.Applications.Handlers.Notes.Commands.Update;
using ToDoList.Application.Applications.Handlers.Notes.Commands.UpdateCategory;
using ToDoList.Application.Applications.Handlers.Notes.Commands.UpdateDueDate;
using ToDoList.Application.Common.Exceptions;
using ToDoList.Domain;
using ToDoList.Infrastructure;
using ToDoList.Services.Abstract;

namespace ToDoList.Application.Applications.Services;

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
            throw new NotFoundException("Такая задача не найдена");
        
        return note;
    }

    public async Task AddNoteAsync(CreateNoteRequest request, CancellationToken cancellationToken)
    {
        var note = new Note(request.Title, request.Description, request.Category);
        await context.Notes.AddAsync(note, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateNoteAsync(UpdateNoteRequest request, CancellationToken cancellationToken)
    {
        var updateNote = await context.Notes
            .Where(n => n.Id == request.Id)
            .ExecuteUpdateAsync(setters => setters
                    .SetProperty(n => n.Title, request.Data.Title)
                    .SetProperty(n => n.Description, request.Data.Title)
                    .SetProperty(n => n.IsCompleted, request.Data.IsCompleted),
                cancellationToken);

        if (updateNote == 0)
            throw new NotFoundException("Такая задача не найдена");

    }

    public async Task CompleteNoteAsync(int id, CancellationToken cancellationToken)
    {
        var note = await context.Notes.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        
        if (note is null)
            throw new NotFoundException("Такая задача не найдена");

        note.IsCompleted = true;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateDueDateAsync(UpdateDueDateRequest request, CancellationToken cancellationToken)
    {
        var note = await context.Notes.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        
        if (note is null) 
            throw new NotFoundException("Такая задача не найдена");

        note.DueDate = request.Data.NewDueDate;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCategoryAsync(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var note = await context.Notes.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        
        if (note is null) 
            throw new NotFoundException("Такая задача не найдена");

        note.Category = request.Data.Category;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteNoteAsync(int id, CancellationToken cancellationToken)
    {
        var note = await context.Notes.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        
        if (note is null)
            throw new NotFoundException("Такая задача не найдена");

        context.Notes.Remove(note);
        await context.SaveChangesAsync(cancellationToken);
    }
}