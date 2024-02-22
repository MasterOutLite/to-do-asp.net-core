using Domain.Entities;

namespace Application.Abstractions.Interfaces;

public interface IJwtProvider
{
    string CreateToken(ApplicationUser user, IEnumerable<string> roles);
}