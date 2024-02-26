namespace Application.ToDos.Queries.GetToDoList;

public sealed class GetToDoListQueryValidation: AbstractValidator<GetToDoListQuery>
{
    public GetToDoListQueryValidation()
    {
        RuleFor(model => model.Count)
            .GreaterThan(0);
    }
}