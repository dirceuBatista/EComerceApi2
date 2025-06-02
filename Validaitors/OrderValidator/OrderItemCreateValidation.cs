using FluentValidation;
using LivrariaApi.Data;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;
using LivrariaApi.ViewModels.InputOrder;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Valideitors;

public class OrderItemCreateValidation : AbstractValidator<InputOrderItemCreate>
{
    private readonly AppDbContext _context;
    public OrderItemCreateValidation(AppDbContext context)
    {
        _context = context;
        
        RuleFor(x => x.BookName)
            .NotEmpty().WithMessage("O nome do livro é obrigatório.")
            .MinimumLength(2).WithMessage("O nome do livro deve conter ao menos 2 caracteres.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
    }
    private async Task<bool> BookExists(string bookName, CancellationToken cancellationToken)
    {
        return await _context.Books.AnyAsync(b => b.Name == bookName, cancellationToken);
    }


}