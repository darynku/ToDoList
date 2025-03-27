namespace ToDoList.Domain;

public class Note
{
    public Note(string title, string description, string category)
    {
        Title = title;
        Description = description;
        IsCompleted = false;
        Category = category;
        Created = DateTime.UtcNow;
    }
    public int Id { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime Created { get; set; } 
    public DateTime? DueDate { get; set; } 
    public string Category { get; set; }
}