namespace Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>, IValidationProperty;

public interface ICommand : IRequest, IValidationProperty;
