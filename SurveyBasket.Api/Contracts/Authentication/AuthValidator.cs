namespace SurveyBasket.Api.Contracts.Authentication;

public class AuthValidator : AbstractValidator<AuthRequest>
{
    public AuthValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty();
        RuleFor(r => r.Password)
            .NotEmpty();
    }
}
