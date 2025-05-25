namespace LivrariaApi.ViewModels.InputViewModel;

public class InputUserUpdate
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public InputCustomerUpdate? Customer { get; set; }
}