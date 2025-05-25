namespace LivrariaApi.Models;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public string? Document { get; set; }
    
    public string? Phone { get; set; }
    public List<Order> Orders { get; set; } = new List<Order>();

}