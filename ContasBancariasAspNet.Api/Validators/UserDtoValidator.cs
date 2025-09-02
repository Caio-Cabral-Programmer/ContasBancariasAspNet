using ContasBancariasAspNet.Api.DTOs;
using FluentValidation;

namespace ContasBancariasAspNet.Api.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        When(x => x.Account != null, () =>
        {
            RuleFor(x => x.Account!.Number)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(20).WithMessage("Account number must not exceed 20 characters");

            RuleFor(x => x.Account!.Agency)
                .MaximumLength(10).WithMessage("Agency must not exceed 10 characters");

            RuleFor(x => x.Account!.Balance)
                .GreaterThanOrEqualTo(0).WithMessage("Balance must be greater than or equal to 0");

            RuleFor(x => x.Account!.Limit)
                .GreaterThanOrEqualTo(0).WithMessage("Limit must be greater than or equal to 0");
        });

        When(x => x.Card != null, () =>
        {
            RuleFor(x => x.Card!.Number)
                .NotEmpty().WithMessage("Card number is required")
                .MaximumLength(16).WithMessage("Card number must not exceed 16 characters");

            RuleFor(x => x.Card!.Limit)
                .GreaterThanOrEqualTo(0).WithMessage("Card limit must be greater than or equal to 0");
        });
    }
}