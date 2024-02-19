using Application.Abstractions.Messaging;

namespace Application.Auth.Command.RegistrationUser;

public record RegistrationUserCommand(string Email, string UserName, string Password) : ICommand<string>;