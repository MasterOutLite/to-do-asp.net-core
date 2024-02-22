using Application.Abstractions.Messaging;

namespace Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(long Id, long UserId) : ICommand<bool>;

public record DeleteCategoryRequest(long Id);