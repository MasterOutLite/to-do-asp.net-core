namespace Application.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidation()
    {
        RuleFor(model => model.Description)
            .NotEmpty();

        RuleFor(model => model.Name)
            .NotEmpty();
    }
}