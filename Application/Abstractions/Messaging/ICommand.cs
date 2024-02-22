using MediatR;

namespace Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>, ICommandBase;

public interface ICommand : IRequest, ICommandBase;

public interface ICommandBase;