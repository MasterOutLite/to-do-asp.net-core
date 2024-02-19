using Application.Abstractions.Messaging;
using Application.Abstractions.Models;

namespace Application.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(long Id, long UserId) : IQuery<CategoryResponse>;