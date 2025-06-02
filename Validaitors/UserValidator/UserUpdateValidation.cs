using FluentValidation;
using LivrariaApi.ViewModels.InputViewModel;

namespace LivrariaApi.Valideitors;

public class UserUpdateValidation : AbstractValidator<InputUserUpdate>
{
    public UserUpdateValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail deve ser válido.");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");

        When(x => x.Customer != null, () =>
        {
            RuleFor(x => x.Customer!.Document)
                .NotEmpty().WithMessage("O documento do cliente é obrigatório.")
                .Length(11).WithMessage("O documento deve ter 11 dígitos.");

            RuleFor(x => x.Customer!.Phone)
                .NotEmpty().WithMessage("O telefone do cliente é obrigatório.")
                .Matches(@"^\d{10,11}$").WithMessage("O telefone deve conter entre 10 e 11 dígitos.");
        });
    }
}
