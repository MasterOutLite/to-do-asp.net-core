using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using Serilog;

namespace Application.ToDos.Commands.CreateToDo;

public class CreateToDoCommandHandler : ICommandHandler<CreateToDoCommand, long>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateToDoCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<long> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Category
            .Where(category => category.Id == request.CategoryId)
            .FirstOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            throw new NotFoundCategoryException(request.CategoryId);
        }

        var entity = request.Adapt<ToDo>();
        Log.Information("Entity {@Entity}. Dto {@Dto}", entity, request);

        _dbContext.ToDo.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}