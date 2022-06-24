using FluentValidation;

namespace M13.InterviewProject.Application.Consumers.SpellChecker.Queries.GetErrorsCountFromPage;

public class GetErrorsCountFromPageQueryValidator: AbstractValidator<GetErrorsCountFromPageQuery>
{
    public GetErrorsCountFromPageQueryValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty();
        RuleFor(x => x.Url)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.Url))
            .WithMessage("Incorrect Url");
    }
}