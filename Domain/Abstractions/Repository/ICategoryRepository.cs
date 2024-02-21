using Domain.Entities;

namespace Domain.Abstractions.Repository;

public interface ICategoryRepository
{
    void Add(Category category);
    void Remove(Category category);
    void Delete(Category category)
    Category? GetById(int id);
}