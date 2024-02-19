using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Application.Common.Models;
using Mapster;
using Serilog;

namespace Application.ToDos.Queries.GetToDoList;

public record GetToDoListQuery(int page, int count) : IQuery<List<ToDoResponse>>;

public class GetToDoListHandler : IQueryHandler<GetToDoListQuery, List<ToDoResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetToDoListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ToDoResponse>> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
    {
        int skip = request.page * request.count;

        var toDos = await _context.ToDo
            .Skip(skip)
            .Take(request.count)
            .ToListAsync();

        return toDos.Adapt<List<ToDoResponse>>();
    }
}