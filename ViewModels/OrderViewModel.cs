namespace LivrariaApi.ViewModels;

public class OrderViewModel
{
    public Guid CustomerId { get; set; }
    public List<OrderItemViewModel> OrderItems { get; set; }
}