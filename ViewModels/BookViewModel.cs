using LivrariaApi.Models;

namespace LivrariaApi.ViewModels;

public class BookViewModel
{
    public string Name { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public string Slug { get; set; }
    public List<Category> Category { get; set; }
}