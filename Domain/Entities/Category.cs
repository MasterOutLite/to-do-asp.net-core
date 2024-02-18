using Domain.Common;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;

    public List<ToDo> ToDos { get; set; } = new();

    public long UserId { get; set; }
    public ApplicationUser User { get; set; }
}