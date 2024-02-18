using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser<long>
{
    public List<ToDo> ToDos { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}