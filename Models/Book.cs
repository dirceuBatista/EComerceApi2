using LivrariaApi.ViewModels;

namespace LivrariaApi.Models;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public string Slug { get; set; }
    public List<Category> Category { get; set; }
}