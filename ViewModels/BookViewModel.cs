using LivrariaApi.Models;

namespace LivrariaApi.ViewModels;

public class BookViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public string Slug { get; set; }
    public List<CategoryViewModel> Category { get; set; }
}