namespace Application.ToDos.Queries.GetToDoById;

public sealed class GetToDoByIdQueryValidation : AbstractValidator<GetToDoByIdQuery>
{
    public GetToDoByIdQueryValidation()
    {
        RuleFor(model => model.Id)
            .GreaterThan(0);

        RuleFor(model => model.UserId)
            .GreaterThan(0);
    }
}