using FluentValidation;

namespace M13.InterviewProject.Application.Consumers.Rule.Commands.DeleteRule;

public class DeleteRuleCommandValidator: AbstractValidator<DeleteRuleCommand>
{
    public DeleteRuleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}