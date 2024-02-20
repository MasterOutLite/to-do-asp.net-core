using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        var roles = Enum.GetValues<Role>()
            .Select(role => new ApplicationRole()
            {
                Id = (int)role,
                Name = role.ToString()
            });

        builder.HasData(roles);
    }
}