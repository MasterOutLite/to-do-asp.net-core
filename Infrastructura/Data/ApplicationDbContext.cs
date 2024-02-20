using Application.Abstractions.Interfaces;
using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Data
{
    public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>,
        IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Log.Information("Connect to DB");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<ToDo> ToDo { get; set; }
        public DbSet<User> User { get; set; }
    }
}