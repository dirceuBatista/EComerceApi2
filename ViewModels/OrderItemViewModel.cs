using System.Text.Json.Serialization;

namespace LivrariaApi.ViewModels;

public class OrderItemViewModel
{
    
    public Guid BookId { get; set; }
    public string BookName { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}