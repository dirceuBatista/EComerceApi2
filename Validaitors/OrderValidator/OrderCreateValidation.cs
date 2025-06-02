using FluentValidation;
using FluentValidation.Validators;
using LivrariaApi.Data;
using LivrariaApi.ViewModels;
using LivrariaApi.ViewModels.InputOrder;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Valideitors;

public class OrderCreateValidation : AbstractValidator<InputOrderCreate>
{
    public OrderCreateValidation(AppDbContext context)
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(Guid.Empty).WithMessage
                ("O ID do cliente é obrigatório e deve ser maior que zero.");
        async Task<bool> CustomerExists(int customerId, CancellationToken cancellationToken)
        {
            return await context.Customers.AnyAsync(c => Equals(c.Id, customerId), cancellationToken);
        }
        
        RuleFor(x => x.OrderItems)
            .NotEmpty().WithMessage("O pedido deve conter ao menos um item.");

       
            RuleForEach(x => x.OrderItems)
                .SetValidator(new OrderItemCreateValidation(context));
    }
}


    
