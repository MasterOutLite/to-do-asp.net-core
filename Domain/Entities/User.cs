using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public List<ToDo> toDos { get; set; }
    public List<Category> categorys { get; set; }
}