namespace LivrariaApi.ViewModels.InputOrder;

public class InputOrderItemUpdate
{
    public Guid OrderId { get; set; }
    public Guid BookId { get; set; }
    public string BookName{ get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
        
    public decimal Total { get; set; }
}