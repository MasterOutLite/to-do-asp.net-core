using Domain.Entities;

namespace Domain.Abstractions.Repository;

public interface ICategoryRepository
{
    void Add(Category category);
    void Remove(Category category);
    void Update(Category category);
    Task<Category?> GetById(long id);
    Task<Category?> GetByIdAndUserId(long id, long userId);
    Task<IEnumerable<Category>> GetAllByUserId(long userId);

    Task<Category?> GetByIdAndUserIdWithRelation(long id, long userId);
}