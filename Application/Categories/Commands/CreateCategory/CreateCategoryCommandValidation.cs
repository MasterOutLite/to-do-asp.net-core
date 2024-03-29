﻿namespace Application.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidation()
    {
        RuleFor(model => model.Name)
            .NotEmpty();

        RuleFor(model => model.Description)
            .NotEmpty();
    }
}