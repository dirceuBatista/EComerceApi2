using System.Text.Json.Serialization;
using LivrariaApi.Models;

namespace LivrariaApi.ViewModels;

public class UserViewModel
{
    public Guid Id{ get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Slug { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public CustomerViewModel? Customer { get; set; }
    
}