using FluentValidation;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;

namespace LivrariaApi.Valideitors;

public class UserValidation : AbstractValidator<UserViewModel>
{
    public UserValidation()
    {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.");

        RuleFor(user => user.Slug)
            .NotEmpty().WithMessage("O slug é obrigatório.");


    }
}