namespace LivrariaApi.ViewModels.InputOrder;

public class InputOrderUpdate
{
    
    public Guid CustomerId { get; set; } 

    public IList<InputOrderItemUpdate> OrderItems { get; set; } 

}