using System.Linq.Expressions;
using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Application.Common.Models;
using Domain.Entities;
using Mapster;
using Serilog;

namespace Application.ToDos.Queries.GetToDoList;

public class GetToDoListHandler : IQueryHandler<GetToDoListQuery, PaginationResponse<ToDoResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetToDoListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginationResponse<ToDoResponse>> Handle(GetToDoListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.ToDo.Where(e => e.UserId == request.UserId &&
                                             (!request.CategoryId.HasValue || e.CategoryId == request.CategoryId)
        );

        Expression<Func<ToDo, object>> keyFilter = request.SortColumn switch
        {
            ToDoColumnSort.Title => o => o.Title,
            ToDoColumnSort.Category => o => o.CategoryId,
            ToDoColumnSort.Done => o => o.Done,
            _ => o => o.Id,
        };

        if (request.SortOrder)
            query = query.OrderBy(keyFilter);
        else
            query = query.OrderByDescending(keyFilter);

        int skip = request.Page * request.Count;

        var toDos = await query
            .Skip(skip)
            .Take(request.Count)
            .ToListAsync(cancellationToken);

        int total = await query.CountAsync(cancellationToken);

        var toDoResponses = toDos.Adapt<List<ToDoResponse>>();

        //Log.Information("Total: {total}. ToDo: {@toDo}.ResponseToDo: {@response}",
        //    total, toDos, toDoResponses);

        return new PaginationResponse<ToDoResponse>(request.Page, total, toDoResponses);
    }
}