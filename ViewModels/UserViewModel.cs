using LivrariaApi.Models;

namespace LivrariaApi.ViewModels;

public class UserViewModel
{
    public Guid Id{ get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Slug { get; set; }
    public Customer Customer { get; set; }
    public List<Role> Roles { get; set; }
}