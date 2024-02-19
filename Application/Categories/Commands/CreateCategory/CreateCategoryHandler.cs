using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Domain.Exceptions;
using Mapster;

namespace Application.Categories.Commands.CreateCategory;

public sealed class CreateCategoryHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateCategoryCommand, long>
{
    public async Task<long> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.User
            .Where(e => e.Id == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new NotFoundUserException(request.UserId);
        }

        var entity = request.Adapt<Domain.Entities.Category>();
        dbContext.Category.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}