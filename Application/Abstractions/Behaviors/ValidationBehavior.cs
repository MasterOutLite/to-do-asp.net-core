using System.ComponentModel.DataAnnotations;
using Application.Abstractions.Messaging;

namespace Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
{

    //private readonly IEnumerable<IValidator<TRequest> _validators;


    public Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}