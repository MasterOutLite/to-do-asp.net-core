using Application.Abstractions.Messaging;
using Domain.Exceptions;

namespace Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IValidationProperty
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var errors = validationFailures
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .Select(res => new ValidationErrors(
                res.PropertyName,
                res.ErrorMessage
            ))
            .ToList();

        if (errors.Any())
        {
            throw new Domain.Exceptions.ValidationException(errors);
        }

        return await next();
    }
}