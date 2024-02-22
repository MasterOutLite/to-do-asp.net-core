namespace Application.ToDos.Commands.CreateToDo;

public sealed class CreateToDoCommandValidator : AbstractValidator<CreateToDoCommand>
{
    public CreateToDoCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotEmpty()
            .WithMessage("UserId empty");

        RuleFor(c => c.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId empty");

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("Description empty");

        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("Title empty");
        //RuleFor(c => c.Done).Custom()
    }
}