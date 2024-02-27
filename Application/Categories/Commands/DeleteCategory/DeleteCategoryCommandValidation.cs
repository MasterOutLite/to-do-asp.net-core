using Domain.Abstractions.Repository;

namespace Application.Categories.Commands.DeleteCategory;

public sealed class DeleteCategoryCommandValidation : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidation(ICategoryRepositoryQuery query)
    {
        RuleFor(model => model.Id)
            .GreaterThan(0);
    }
}