using Domain.Entities;

namespace Domain.Abstractions.Repository;

public interface ICategoryRepositoryQuery
{
    Task<Category?> GetById (long id);
    Task<Category?> GetByIdAndUserId (long id, long userId);
    Task<IEnumerable<Category>> GetAllByUserId (long userId);
    Task<Category?> GetByIdAndUserIdWithRelation (long id, long userId);
}