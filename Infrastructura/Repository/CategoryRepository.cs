using Application.Abstractions.Interfaces;
using Domain.Abstractions.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class CategoryRepository(IApplicationDbContext context) : ICategoryRepository
{
    public void Add(Category category)
    {
        context.Category.Add(category);
    }

    public void Remove(Category category)
    {
        context.Category.Remove(category);
    }

    public void Update(Category category)
    {
        context.Category
            .Update(category);
    }
}