using Application.Abstractions.Messaging;
using Application.Common.Models;

namespace Application.Categories.Queries.GetCategory;

public record GetCategoryQuery(long UserId) : IQuery<IEnumerable<CategoryResponse>>;