using Application.Abstractions.Messaging;

namespace Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string Description, long UserId) : ICommand<long>;