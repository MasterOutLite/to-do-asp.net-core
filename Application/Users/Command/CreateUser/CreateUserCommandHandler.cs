using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Mapster;

namespace Application.Users.Command.CreateUser;

public class CreateUserCommandHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateUserCommand, UserResponse>
{
    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.Adapt<User>();
        dbContext.User.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        return user.Adapt<UserResponse>();
    }
}