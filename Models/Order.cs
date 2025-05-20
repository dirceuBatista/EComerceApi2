namespace LivrariaApi.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime OrderDate { get; set; }
    public Guid CustomerId { get; set; } 
    public Customer? Customer { get; set; }

    public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}