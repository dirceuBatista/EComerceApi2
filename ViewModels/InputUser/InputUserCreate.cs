using System.ComponentModel.DataAnnotations;

namespace LivrariaApi.ViewModels.InputViewModel;

public class InputUserCreate
{
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public InputCustomerCreate? Customer { get; set; }
}
