
namespace SurveyBasket.Api.Contracts.Validation;

public class PollRequestValidator : AbstractValidator<PollRequest>
{
    public PollRequestValidator()
    {
        RuleFor(P => P.Name)
            .NotEmpty()
            .WithMessage("Name is required Gays.");
    }
}
