using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Application.Common.Models;
using Domain.Abstractions.Repository;
using Mapster;

namespace Application.Categories.Commands.CreateCategory;

public sealed class CreateCategoryHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateCategoryCommand, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<Domain.Entities.Category>();
        categoryRepository.Add(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Adapt<CategoryResponse>();
    }
}