namespace Application.ToDos.Commands.CreateToDo;

public sealed class CreateToDoCommandValidator : AbstractValidator<CreateToDoCommand>
{
    public CreateToDoCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(c => c.CategoryId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull();
        //RuleFor(c => c.Done).Custom()
    }
}