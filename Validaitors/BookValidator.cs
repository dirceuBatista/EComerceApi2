using LivrariaApi.ViewModels;

namespace LivrariaApi.Valideitors;

using FluentValidation;

public class BookValidator : AbstractValidator<BookViewModel>
{
    public BookValidator()
    {
        RuleFor(book => book.Name)
            .NotEmpty().WithMessage("O nome do livro é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do livro deve ter no máximo 100 caracteres.");

        RuleFor(book => book.Author)
            .NotEmpty().WithMessage("O autor é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do autor deve ter no máximo 100 caracteres.");

        RuleFor(book => book.Price)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

        RuleFor(book => book.Slug)
            .NotEmpty().WithMessage("O slug é obrigatório.")
            .Matches("^[a-z0-9]+(?:-[a-z0-9]+)*$").WithMessage("O slug deve estar em formato 'slug-friendly' (minúsculas e hífens).");

        RuleFor(book => book.Category)
            .NotNull().WithMessage("A categoria é obrigatória.")
            .Must(c => c.Count > 0).WithMessage("Deve haver pelo menos uma categoria.");
    }
}
