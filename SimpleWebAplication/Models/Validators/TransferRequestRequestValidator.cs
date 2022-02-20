using FluentValidation;

namespace SimpleWebAplication.Validators
{
    public class TransferRequestRequestValidator : AbstractValidator<TransferRequest>
    {
        public TransferRequestRequestValidator()
        {
            RuleFor(r => r.Target).NotNull();
            RuleFor(r => r.Origin).NotNull();

            RuleFor(r => r.Origin.Bank).NotEmpty();
            RuleFor(r => r.Origin.Branch).NotEmpty();
            RuleFor(r => r.Origin.Cpf).NotEmpty();

            RuleFor(r => r.Target.Bank).NotEmpty();
            RuleFor(r => r.Target.Branch).NotEmpty();
            RuleFor(r => r.Target.Account).NotEmpty();

            RuleFor(r => r.Event).NotEmpty();
            RuleFor(r => r.Amount).GreaterThan(0);
        }
    }
}