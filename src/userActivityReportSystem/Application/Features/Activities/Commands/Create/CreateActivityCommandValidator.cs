using FluentValidation;

namespace Application.Features.Activities.Commands.Create;

public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.ActivityType).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}