using Domain.Common;

namespace Domain.Entities;

public class ToDo : BaseEntity
{
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public bool Done { get; set; } = false;

    public long CategoryId { get; set; }
    public Category Category { get; set; }

    public long UserId { get; set; }
    public ApplicationUser User { get; set; }
}