using FluentValidation;

namespace Project.Core.Tenant.Features.UpgradeSubscription;
public class UpgradeSubscriptionValidator : AbstractValidator<UpgradeSubscriptionCommand>
{
    public UpgradeSubscriptionValidator()
    {
        RuleFor(t => t.Tenant).NotEmpty();
        RuleFor(t => t.ExtendedExpiryDate).GreaterThan(DateTime.UtcNow);
    }
}
