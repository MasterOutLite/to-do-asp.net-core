using Application.Abstractions.Messaging;
using Application.Common.Models;

namespace Application.ToDos.Queries.GetToDoList;

public record GetToDoListQuery(
    int Page,
    int Count,
    long UserId,
    bool SortOrder,
    long? CategoryId,
    ToDoColumnSort? SortColumn)
    : IQuery<PaginationResponse<ToDoResponse>>;

public sealed record GetToDoListRequest(
    int Page,
    int Count,
    bool SortOrder,
    long? CategoryId,
    ToDoColumnSort? SortColumn
);