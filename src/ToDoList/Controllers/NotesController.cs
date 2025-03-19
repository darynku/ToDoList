using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoList.Contracts;
using ToDoList.Domain;
using ToDoList.Services.Abstract;

namespace ToDoList.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag("Контроллер для работы с задачами")]
public class NotesController(INoteService noteService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetAllNotes()
    {
        var notes = await noteService.GetAllNotesAsync();
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> GetNoteById(int id, CancellationToken cancellationToken)
    {
        var note = await noteService.GetNoteByIdAsync(id, cancellationToken);
        return Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> AddNote([FromBody] CreateNoteRequest request , CancellationToken cancellationToken)
    {
        await noteService.AddNoteAsync(request, cancellationToken);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, [FromBody] UpdateNoteRequest request, CancellationToken cancellationToken)
    {
        await noteService.UpdateNoteAsync(id, request, cancellationToken);
        return Ok();
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> CompleteNote(int id, CancellationToken cancellationToken)
    {
        await noteService.CompleteNoteAsync(id, cancellationToken);
        return Ok();
    }

    [HttpPatch("{id}/dueDate")]
    public async Task<IActionResult> UpdateDueDate(int id, [FromBody] UpdateDueDateRequest request, CancellationToken cancellationToken)
    {
        await noteService.UpdateDueDateAsync(id, request, cancellationToken);
        return Ok();
    }

    [HttpPatch("{id}/category")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        await noteService.UpdateCategoryAsync(id, request, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id, CancellationToken cancellationToken)
    {
        await noteService.DeleteNoteAsync(id, cancellationToken);
        return Ok();
    }
}
