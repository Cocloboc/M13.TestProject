using FluentValidation;

namespace M13.InterviewProject.Application.Consumers.SpellChecker.Queries.GetErrorsFromText;

public class GetErrorsFromTextQueryValidator: AbstractValidator<GetErrorsFromTextQuery>
{
    public GetErrorsFromTextQueryValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty();
    }
}