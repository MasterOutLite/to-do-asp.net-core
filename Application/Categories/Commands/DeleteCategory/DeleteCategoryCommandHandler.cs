using Application.Abstractions.Interfaces;
using Application.Abstractions.Messaging;
using Domain.Abstractions.Repository;

namespace Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<DeleteCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAndUserIdWithRelation(request.Id, request.UserId);

        if (category is null || category.ToDos.Capacity != 0)
        {
            return false;
        }

        categoryRepository.Remove(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}