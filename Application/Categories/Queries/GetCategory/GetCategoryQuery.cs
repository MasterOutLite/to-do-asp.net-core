using Application.Abstractions.Messaging;
using Application.Abstractions.Models;

namespace Application.Categories.Queries.GetCategoryUser;

public record GetCategoryQuery(long UserId): IQuery<IEnumerable<CategoryResponse>>;