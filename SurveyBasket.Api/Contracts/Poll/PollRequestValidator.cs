namespace SurveyBasket.Api.Contracts.Poll;

public class PollRequestValidator : AbstractValidator<PollRequest>
{
    public PollRequestValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(p => p.Summary)
            .NotEmpty()
            .MaximumLength(1500);


        RuleFor(p => p.StartsAt)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

        RuleFor(p => p.EndsAt)
            .NotEmpty();

        RuleFor(p => p)
            .Must(p => p.EndsAt >= p.StartsAt);
            
    }
}
