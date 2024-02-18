using Application.Abstractions.Messaging;
using Application.Common.Models;

namespace Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string Description, long UserId) : ICommand<long>;