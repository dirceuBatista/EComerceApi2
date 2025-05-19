namespace LivrariaApi.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public List<Role> Roles { get; set; }
    public Customer customer { get; set; }
    
}