namespace LivrariaApi.Models;

public class OrderItem
{
    public Guid Id { get; set; }= Guid.NewGuid();
   
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public Guid BookId { get; set; }
    public string BookName{ get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
        
    public decimal Total { get; set; }

}