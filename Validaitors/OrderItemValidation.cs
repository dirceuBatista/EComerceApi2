using FluentValidation;
using LivrariaApi.Data;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Valideitors;

public class OrderItemValidation : AbstractValidator<OrderItemViewModel>
{
    public OrderItemValidation(AppDbContext context)
    {
            
        RuleFor(x => x.BookId)
            .GreaterThan(Guid.Empty).WithMessage("O ID do produto é obrigatório e deve ser maior que zero.");
        RuleFor(x => x.BookName)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");

         async Task<dynamic> BeCorrectPrice(OrderItem item, decimal unitPrice, CancellationToken ct)
        {
            var book = await context.Books
                .FirstOrDefaultAsync(x => x.Id == item.BookId, ct);

            if (book == null)
                return false;
            return unitPrice = book.Price;
        }
    }


}