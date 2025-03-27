namespace ToDoList.Application.Conracts.Response;

public class NoteResponse
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; } 
    public required string Category { get; set; }
}