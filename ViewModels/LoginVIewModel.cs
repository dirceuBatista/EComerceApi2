using System.ComponentModel.DataAnnotations;

namespace LivrariaApi.ViewModels;

public class LoginVIewModel
{
    [Required(ErrorMessage = "O email e requerido")]
    [EmailAddress(ErrorMessage = "Email invalido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "A senha e requerida")]
    public string Password { get; set; }
}