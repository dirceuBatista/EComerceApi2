using System.Security.Principal;
using System.Text.Json.Serialization;
using LivrariaApi.Models;

namespace LivrariaApi.ViewModels;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    
    
}