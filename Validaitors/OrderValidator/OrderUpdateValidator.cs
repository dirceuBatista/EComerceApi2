using FluentValidation;
using LivrariaApi.ViewModels.InputOrder;

namespace LivrariaApi.Valideitors;

public class OrderUpdateValidator: AbstractValidator<InputOrderUpdate>
{
    public OrderUpdateValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("O cliente é obrigatório.");

        RuleFor(x => x.OrderDate)
            .NotEmpty().WithMessage("A data do pedido é obrigatória.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("A data do pedido não pode ser no futuro.");

        RuleFor(x => x.OrderItems)
            .NotNull().WithMessage("Itens do pedido são obrigatórios.")
            .NotEmpty().WithMessage("É necessário pelo menos um item no pedido.");

        RuleForEach(x => x.OrderItems).SetValidator(new OrderItemUpdateValidator());
    }
}

    
