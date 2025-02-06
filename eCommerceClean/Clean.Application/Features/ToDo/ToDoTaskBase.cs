namespace eCommerceClean.Application.Features.ToDo;

public abstract record class ToDoTaskBase
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Priority { get; init; } = string.Empty;
    public DateTime DueDate { get; init; }
    public bool IsCompleted { get; init; }
}
