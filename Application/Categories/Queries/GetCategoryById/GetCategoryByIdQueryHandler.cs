using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Application.Common.Models;
using Domain.Exceptions;
using Mapster;

namespace Application.Categories.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCategoryByIdQuery, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await dbContext.Category
            .Where(category => category.Id == request.Id && category.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            throw new NotFoundCategoryException(request.Id);
        }

        return category.Adapt<CategoryResponse>();
    }
}