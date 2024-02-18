using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;

    // public List<ToDo> toDos { get; set; }
    // public List<Category> categorys { get; set; }
}