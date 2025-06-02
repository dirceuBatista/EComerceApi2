namespace LivrariaApi.ViewModels.InputOrder;

public class InputOrderUpdate
{
    public DateTime OrderDate { get; set; }
    public Guid CustomerId { get; set; } 

    public IList<InputOrderItemUpdate> OrderItems { get; set; } 
}