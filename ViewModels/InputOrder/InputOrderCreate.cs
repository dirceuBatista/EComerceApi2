namespace LivrariaApi.ViewModels.InputOrder;

public class InputOrderCreate
{
    public Guid CustomerId { get; set; } 
    public List<InputOrderItemCreate> OrderItems { get; set; }
}