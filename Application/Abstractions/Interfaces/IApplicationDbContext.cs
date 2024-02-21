using Domain.Entities;

namespace Application.Abstractions.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<ToDo> ToDo { get; set; }
        public DbSet<User> User { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}