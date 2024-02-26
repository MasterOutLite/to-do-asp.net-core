namespace Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>, IValidationProperty;

public interface IQuery : IRequest, IValidationProperty;