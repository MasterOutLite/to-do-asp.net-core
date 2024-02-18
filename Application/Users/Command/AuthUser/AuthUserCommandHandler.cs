using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using Domain.Exceptions;

namespace Application.Users.Command.AuthUser;

public class AuthUserCommandHandler(IApplicationDbContext dbContext)
    : ICommandHandler<AuthUserCommand, string>
{
    public async Task<string> Handle(AuthUserCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.User
            .Where(e => e.Email == request.Email && e.Password == request.Password)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new UnauthorizedFailLoginException();
        }

        return "";
    }
}