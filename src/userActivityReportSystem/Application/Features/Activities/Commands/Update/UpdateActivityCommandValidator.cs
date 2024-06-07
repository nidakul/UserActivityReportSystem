using FluentValidation;

namespace Application.Features.Activities.Commands.Update;

public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
    public UpdateActivityCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.ActivityType).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}