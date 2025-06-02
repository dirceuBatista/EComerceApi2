using LivrariaApi.Models;

namespace LivrariaApi.ViewModels;

public class CategoryViewModel
{

    public string Name { get; set; }
    public string Slug { get; set; }
    public List<BookViewModel> Books { get; set; }
}