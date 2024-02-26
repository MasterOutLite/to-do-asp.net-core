namespace Application.Auth.Command.RegistrationUser;

public sealed class RegistrationUserCommandValidation : AbstractValidator<RegistrationUserCommand>
{
    public RegistrationUserCommandValidation()
    {
        RuleFor(model => model.Email)
            .EmailAddress();

        RuleFor(model => model.Password)
            .MinimumLength(8);

        RuleFor(model => model.UserName)
            .MinimumLength(4);
    }
}