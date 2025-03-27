namespace ToDoList.Application.Conracts.Requests;

public class NoteData
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; } 
    public required string Category { get; set; }
}