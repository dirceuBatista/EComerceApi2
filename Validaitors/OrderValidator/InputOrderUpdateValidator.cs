using FluentValidation;
using LivrariaApi.Data;
using LivrariaApi.ViewModels.InputOrder;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Valideitors;

public class OrderUpdateValidator : AbstractValidator<InputOrderUpdate>
{
    private readonly AppDbContext _context;

    public OrderUpdateValidator(AppDbContext context)
    {
        _context = context;

        RuleFor(x => x.CustomerId)
            .NotEqual(Guid.Empty).WithMessage("O ID do cliente é obrigatório.")
            .MustAsync(CustomerExists).WithMessage("O cliente especificado não existe.");

        RuleFor(x => x.OrderItems)
            .NotNull().WithMessage("A lista de itens não pode ser nula.")
            .NotEmpty().WithMessage("O pedido deve conter ao menos um item.");

        RuleForEach(x => x.OrderItems)
            .SetValidator(new OrderItemUpdateValidator(_context));
    }

    private async Task<bool> CustomerExists(Guid customerId, CancellationToken cancellationToken)
    {
        return await _context.Customers.AnyAsync(c => c.Id == customerId, cancellationToken);
    }
}
