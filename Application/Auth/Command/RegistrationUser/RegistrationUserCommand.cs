using Application.Abstractions.Messaging;
using Application.Common.Models;

namespace Application.Auth.Command.RegistrationUser;

public record RegistrationUserCommand(string Email, string UserName, string Password)
    : ICommand<ResponseToken>;