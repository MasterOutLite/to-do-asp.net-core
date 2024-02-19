using Application.Abstractions.Messaging;
using Application.Common.Models;

namespace Application.Auth.Command.Login;

public record LoginCommand(string Email, string Password) : ICommand<ResponseToken>;