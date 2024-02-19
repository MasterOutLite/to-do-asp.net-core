using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Application.Abstractions.Interfaces;
using Domain.Common;

namespace Infrastructure.Data;

public class DbRepository(IApplicationDbContext context) : IDbRepository
{
    private readonly ApplicationDbContext _context = context as ApplicationDbContext;

    public async Task<long> Add<T>(T newEntity) where T : BaseEntity
    {
        var entity = await _context.Set<T>().AddAsync(newEntity);
        return entity.Entity.Id;
    }

    public async Task AddRange<T>(IEnumerable<T> newEntities) where T : BaseEntity
    {
        await _context.Set<T>().AddRangeAsync(newEntities);
    }

    public async Task Delete<T>(long id) where T : BaseEntity
    {
        var activeEntity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        if (activeEntity is null)
        {
            return;
        }

        activeEntity.IsDeleted = true;
        _context.Update(activeEntity);
    }

    public void Remove<T>(T entity) where T : BaseEntity
    {
        _context.Set<T>().Remove(entity);
    }

    public void RemoveRange<T>(IEnumerable<T> entities) where T : BaseEntity
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public void Update<T>(T entity) where T : BaseEntity
    {
        _context.Set<T>().Update(entity);
    }

    public void UpdateRange<T>(IEnumerable<T> entities) where T : BaseEntity
    {
        _context.Set<T>().UpdateRange(entities);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public IQueryable<T> Get<T>() where T : BaseEntity
    {
        return _context.Set<T>().Where(x => !x.IsDeleted).AsQueryable();
    }

    public IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T : BaseEntity
    {
        return _context.Set<T>().Where(selector).Where(x => !x.IsDeleted).AsQueryable();
    }

    public IQueryable<T> GetAll<T>() where T : BaseEntity
    {
        return _context.Set<T>().AsQueryable();
    }
}