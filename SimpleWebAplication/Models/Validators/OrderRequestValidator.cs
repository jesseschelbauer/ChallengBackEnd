using FluentValidation;
using SimpleWebAplication.Models;

namespace SimpleWebAplication.Validators
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            RuleFor(r => r.Symbol).NotEmpty();
            RuleFor(r => r.Amount).GreaterThan(0);
        }
    }
}