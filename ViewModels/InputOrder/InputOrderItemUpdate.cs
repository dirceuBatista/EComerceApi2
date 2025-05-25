namespace LivrariaApi.ViewModels.InputOrder;

public class InputOrderItemUpdate
{
    public Guid BookId { get; set; }
    public string BookName{ get; set; }
    public int Quantity { get; set; }
   
}