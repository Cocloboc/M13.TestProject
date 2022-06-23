using FluentValidation;

namespace M13.InterviewProject.Application.Consumers.Rule.Queries.TestRule;

public class GetTextFromPageQueryValidator: AbstractValidator<GetTextFromPageQuery>
{
    public GetTextFromPageQueryValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty();
        RuleFor(x => x.Url)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.Url))
            .WithMessage("Incorrect Url");
    }
}