namespace Application.Auth.Command.Login;

public sealed class LoginCommandValidation : AbstractValidator<LoginCommand>
{
    public LoginCommandValidation()
    {
        RuleFor(model => model.Email)
            .EmailAddress();

        RuleFor(model => model.Password)
            .MinimumLength(8);
    }
}