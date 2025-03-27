using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoList.Application.Applications.Handlers.Notes.Commands.CompleteNote;
using ToDoList.Application.Applications.Handlers.Notes.Commands.Create;
using ToDoList.Application.Applications.Handlers.Notes.Commands.Detele;
using ToDoList.Application.Applications.Handlers.Notes.Commands.Update;
using ToDoList.Application.Applications.Handlers.Notes.Commands.UpdateCategory;
using ToDoList.Application.Applications.Handlers.Notes.Commands.UpdateDueDate;
using ToDoList.Application.Applications.Handlers.Notes.Queries.GetNote;
using ToDoList.Application.Applications.Handlers.Notes.Queries.GetNotesPaged;
using ToDoList.Application.Conracts.Requests;

namespace ToDoList.Controllers;

/// <inheritdoc />
[ApiController]
[Route("api/[controller]")]
[SwaggerTag("Контроллер для работы с задачами")]
public class NotesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllNotes([FromQuery] GetNotesQuery request, CancellationToken cancellationToken)
    {
        var notes =  await mediator.Send(request, cancellationToken);
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetNoteById(int id, CancellationToken cancellationToken)
    {
        var request = new GetNoteById(id);
        var note = await mediator.Send(request, cancellationToken);
        return Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> AddNote([FromBody] CreateNoteRequest request , CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote([FromRoute] int id, [FromBody] NoteData data, CancellationToken cancellationToken)
    {
        var command = new UpdateNoteRequest(id, data);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> CompleteNote(int id, CancellationToken cancellationToken)
    {
        var command = new CompleteNoteRequest(id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPatch("{id}/dueDate")]
    public async Task<IActionResult> UpdateDueDate(int id, [FromBody] NoteDueDateData data, CancellationToken cancellationToken)
    {
        var command = new UpdateDueDateRequest(id, data);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPatch("{id}/category")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] NoteCategoryData data, CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryRequest(id, data);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteNoteRequest(id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}

