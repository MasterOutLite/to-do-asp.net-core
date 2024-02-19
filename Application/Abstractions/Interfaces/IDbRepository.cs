using System.Linq.Expressions;
using Domain.Common;

namespace Application.Abstractions.Interfaces;

public interface IDbRepository
{
    IQueryable<T> Get<T>(Expression<Func<T, bool>> selector) where T :  BaseEntity;
    IQueryable<T> Get<T>() where T : BaseEntity;
    IQueryable<T> GetAll<T>() where T : BaseEntity;

    Task<long> Add<T>(T newEntity) where T : BaseEntity;
    Task AddRange<T>(IEnumerable<T> newEntities) where T : BaseEntity;

    Task Delete<T>(long entity) where T : BaseEntity;

    void Remove<T>(T entity) where T : BaseEntity;
    void RemoveRange<T>(IEnumerable<T> entities) where T : BaseEntity;

    void Update<T>(T entity) where T : BaseEntity;
    void UpdateRange<T>(IEnumerable<T> entities) where T : BaseEntity;

    Task<int> SaveChangesAsync();
}