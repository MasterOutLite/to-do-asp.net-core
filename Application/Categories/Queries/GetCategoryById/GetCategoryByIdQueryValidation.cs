namespace Application.Categories.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryValidation : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidation()
    {
        RuleFor(model => model.Id)
            .GreaterThan(0);
    }
}