using Domain.Abstractions.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class CategoryRepositoryQuery(ApplicationDbContext context) : ICategoryRepositoryQuery
{
    public async Task<Category?> GetById(long id)
    {
        return await context.Category
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Category?> GetByIdAndUserId(long id, long userId)
    {
        return await context.Category
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
    }

    public async Task<IEnumerable<Category>> GetAllByUserId(long userId)
    {
        return await context.Category
            .Where(e => e.UserId == userId)
            .OrderBy(category => category.Id)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAndUserIdWithRelation(long id, long userId)
    {
        var category = await context.Category
            .Include(e => e.ToDos)
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

        return category;
    }
}