using Domain.Entities;

namespace Application.Abstractions.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Category> Category { get; }
        public DbSet<ToDo> ToDo { get; }
        public DbSet<User> User { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}