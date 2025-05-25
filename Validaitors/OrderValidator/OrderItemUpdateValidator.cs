using FluentValidation;
using LivrariaApi.Data;
using LivrariaApi.ViewModels.InputOrder;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Valideitors;

public class OrderItemUpdateValidator : AbstractValidator<InputOrderItemUpdate>
{
    private readonly AppDbContext _context;

    public OrderItemUpdateValidator(AppDbContext context)
    {
        _context = context;

        RuleFor(x => x.BookId)
            .NotEqual(Guid.Empty).WithMessage("O ID do livro é obrigatório.")
            .MustAsync(BookExists).WithMessage("O livro especificado não existe.");

        RuleFor(x => x.BookName)
            .NotEmpty().WithMessage("O nome do livro é obrigatório.")
            .MaximumLength(200).WithMessage("O nome do livro deve ter no máximo 200 caracteres.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
        
    }

    private async Task<bool> BookExists(Guid bookId, CancellationToken cancellationToken)
    {
        return await _context.Books.AnyAsync(b => b.Id == bookId, cancellationToken);
    }
}
