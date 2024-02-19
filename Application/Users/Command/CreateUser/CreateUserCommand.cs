using Application.Abstractions.Messaging;
using Application.Abstractions.Models;

namespace Application.Users.Command.CreateUser;

public record CreateUserCommand(string Name, string Email, string Password) : ICommand<UserResponse>;