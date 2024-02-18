using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Log.Information("Connect to DB");
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<ToDo> ToDo { get; set; }
        public DbSet<User> User { get; set; }
    }
}