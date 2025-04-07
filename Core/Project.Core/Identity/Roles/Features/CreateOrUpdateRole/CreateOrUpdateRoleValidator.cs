using FluentValidation;

namespace Project.Core.Identity.Roles.Features.CreateOrUpdateRole;

public class CreateOrUpdateRoleValidator : AbstractValidator<CreateOrUpdateRoleCommand>
{
    public CreateOrUpdateRoleValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Role name is required.");
    }
}
