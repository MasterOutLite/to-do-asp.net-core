using Application.Abstractions.Messaging;
using Application.Common.Models;
using Domain.Abstractions.Repository;
using Domain.Exceptions;
using Mapster;

namespace Application.Categories.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler(
    ICategoryRepository categoryRepository
) : IQueryHandler<GetCategoryByIdQuery, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAndUserId(request.Id, request.UserId);

        if (category is null)
        {
            throw new NotFoundCategoryException(request.Id);
        }

        return category.Adapt<CategoryResponse>();
    }
}