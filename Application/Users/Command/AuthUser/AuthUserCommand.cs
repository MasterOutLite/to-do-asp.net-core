using Application.Abstractions.Messaging;

namespace Application.Users.Command.AuthUser;

public record AuthUserCommand(string Email, string Password) : ICommand<string>;