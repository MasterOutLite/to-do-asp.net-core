using Domain.Abstractions.Repository;

namespace Application.ToDos.Commands.CreateToDo;

public sealed class CreateToDoCommandValidator : AbstractValidator<CreateToDoCommand>
{
    public CreateToDoCommandValidator(ICategoryRepositoryQuery query)
    {
        RuleFor(c => c.UserId)
            .GreaterThan(0);

        RuleFor(c => c.CategoryId)
            .GreaterThan(0)
            .MustAsync(async (command, l, arg3) =>
            {
                var category = await query.GetByIdAndUserId(command.CategoryId, command.UserId);
                return category is not null;
            }).WithMessage("Not found category by id.");

        RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull();
    }
}