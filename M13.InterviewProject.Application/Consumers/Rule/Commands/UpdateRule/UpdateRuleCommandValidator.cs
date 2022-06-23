using FluentValidation;
using M13.InterviewProject.Application.Helpers;

namespace M13.InterviewProject.Application.Consumers.Rule.Commands.UpdateRule;

public class UpdateRuleCommandValidator: AbstractValidator<UpdateRuleCommand>
{
    public UpdateRuleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Value)
            .NotEmpty();
        RuleFor(x => x.Value)
            .Must(value => value.IsXPatchValid())
            .When(x => !string.IsNullOrEmpty(x.Value))
            .WithMessage("Rule is invalid");
    }
}