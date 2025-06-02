namespace LivrariaApi.ViewModels;

public class CustomerViewModel
{
    public Guid UserId { get; set; } 
    public string Name { get; set; }
    public string Document { get; set; }
    public string Phone { get; set; }
}