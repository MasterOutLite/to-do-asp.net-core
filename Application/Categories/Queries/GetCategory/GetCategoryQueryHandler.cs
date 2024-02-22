using Application.Abstractions.Messaging;
using Application.Common.Models;
using Domain.Abstractions.Repository;
using Mapster;

namespace Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler(
    ICategoryRepository categoryRepository
)
    : IQueryHandler<GetCategoryQuery, IEnumerable<CategoryResponse>>
{
    public async Task<IEnumerable<CategoryResponse>> Handle(GetCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var res = await categoryRepository.GetAllByUserId(request.UserId);
        return res.Adapt<List<CategoryResponse>>();
    }
}