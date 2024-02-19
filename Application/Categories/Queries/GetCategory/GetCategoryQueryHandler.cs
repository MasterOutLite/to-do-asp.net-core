using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Application.Abstractions.Models;
using Mapster;

namespace Application.Categories.Queries.GetCategoryUser;

public class GetCategoryQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetCategoryQuery, IEnumerable<CategoryResponse>>
{
    public async Task<IEnumerable<CategoryResponse>> Handle(GetCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var res = await dbContext.Category
            .Where(category => category.UserId == request.UserId)
            .OrderBy(category => category.Id)
            .ToListAsync(cancellationToken);

        return res.Adapt<List<CategoryResponse>>();
    }
}