using FluentValidation;
using LivrariaApi.ViewModels.InputOrder;

namespace LivrariaApi.Valideitors;

public class OrderItemUpdateValidator: AbstractValidator<InputOrderItemUpdate>
{
    public OrderItemUpdateValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("O ID do pedido é obrigatório.");

        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("O ID do livro é obrigatório.");

        RuleFor(x => x.BookName)
            .NotEmpty().WithMessage("O nome do livro é obrigatório.")
            .MaximumLength(200).WithMessage("O nome do livro deve ter no máximo 200 caracteres.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");

        RuleFor(x => x.Total)
            .Must((item, total) => total == item.Quantity * item.UnitPrice)
            .WithMessage("O total deve ser igual à quantidade multiplicada pelo preço unitário.");
    }
}

    
