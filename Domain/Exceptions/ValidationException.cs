using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public class ValidationException : BadRequestException
{
    public ValidationException(IReadOnlyCollection<ValidationErrors> errors)
        : base("Validation failed!")
    {
        Errors = errors;
    }

    public IReadOnlyCollection<ValidationErrors> Errors { get; }
}

public record ValidationErrors(string PropertyName, string ErrorMessage);