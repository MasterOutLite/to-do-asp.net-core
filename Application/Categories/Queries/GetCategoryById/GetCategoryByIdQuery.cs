using Application.Abstractions.Messaging;
using Application.Common.Models;

namespace Application.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(long Id, long UserId)
    : IQuery<CategoryResponse>;

public sealed record GetCategoryByIdRequest(long Id);