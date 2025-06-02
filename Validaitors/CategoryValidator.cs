using FluentValidation;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;

namespace LivrariaApi.Valideitors;

public class CategoryValidator: AbstractValidator<CategoryViewModel>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
            .MaximumLength(100).WithMessage("O nome da categoria deve ter no máximo 100 caracteres.");

        RuleFor(c => c.Slug)
            .NotEmpty().WithMessage("O slug é obrigatório.")
            .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("O slug deve estar em formato válido (minúsculas e hífens).");

        RuleFor(c => c.Books)
            .NotNull().WithMessage("A lista de livros não pode ser nula.")
            .Must(list => list.Count > 0).WithMessage("A categoria deve conter pelo menos um livro.");
        RuleForEach(c => c.Books).SetValidator(new BookValidator());
    }
}

