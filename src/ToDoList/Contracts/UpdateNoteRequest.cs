namespace ToDoList.Contracts;

public record UpdateNoteRequest(string Title, string Description, bool IsCompleted);