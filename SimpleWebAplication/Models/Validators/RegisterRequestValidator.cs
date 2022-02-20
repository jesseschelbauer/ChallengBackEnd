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
            RuleFor(r => r.CPF).NotEmpty();
            RuleFor(r => r.CPF).Length(11);
        }
    }
}