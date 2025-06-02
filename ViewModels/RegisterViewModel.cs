using System.ComponentModel.DataAnnotations;
using LivrariaApi.ViewModels.InputViewModel;

namespace LivrariaApi.ViewModels;

public class RegisterViewModel
{
    public string Name { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public InputCustomerCreate? Customer { get; set; }
}