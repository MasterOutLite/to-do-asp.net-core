using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Category { get; }

        DbSet<ToDo> ToDo { get; }
        DbSet<User> User { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}