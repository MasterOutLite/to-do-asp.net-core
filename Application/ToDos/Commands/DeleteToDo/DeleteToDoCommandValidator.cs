namespace Application.ToDos.Commands.DeleteToDo;

public sealed class DeleteToDoCommandValidator : AbstractValidator<DeleteToDoCommand>
{
    public DeleteToDoCommandValidator()
    {
        RuleFor(model => model.Id)
            .GreaterThan(0);
        ;

        RuleFor(model => model.UserId)
            .GreaterThan(0);
    }
}