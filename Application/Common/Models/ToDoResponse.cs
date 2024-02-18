namespace Application.Common.Models;

public sealed record ToDoResponse(
    long Id,
    string Title,
    string Description,
    bool Done,
    long CategoryId);