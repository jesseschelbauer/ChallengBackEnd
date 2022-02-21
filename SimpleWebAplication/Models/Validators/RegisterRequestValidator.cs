using FluentValidation;
using SimpleWebAplication.Models;

namespace SimpleWebAplication.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.Email).NotEmpty();
            RuleFor(r => r.Email).EmailAddress();
            RuleFor(r => r.Password).NotEmpty();
            RuleFor(r => r.Password).MaximumLength(6);
            RuleFor(r => r.Cpf).NotEmpty();
            RuleFor(r => r.Cpf).Length(11);
        }
    }
}