namespace Application.ToDos.Commands.UpdateToDo;

public class UpdateToDoCommandValidation : AbstractValidator<UpdateToDoCommand>
{
    public UpdateToDoCommandValidation()
    {
        RuleFor(model => model.Id)
            .GreaterThan(0);
    }
}