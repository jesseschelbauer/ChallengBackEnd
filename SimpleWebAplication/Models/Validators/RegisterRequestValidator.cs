using FluentValidation;
using SimpleWebAplication.Models;

namespace SimpleWebAplication.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.Username).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();
            RuleFor(r => r.CPF).NotEmpty();
        }
    }
}