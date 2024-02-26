namespace Application.Categories.Commands.DeleteCategory;

public sealed class DeleteCategoryCommandValidation : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidation()
    {
        RuleFor(model => model.Id)
            .GreaterThan(0);
    }
}