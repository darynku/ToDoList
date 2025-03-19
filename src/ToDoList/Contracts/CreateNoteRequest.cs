namespace ToDoList.Contracts;

public record CreateNoteRequest(string Title, string Description, string Category);