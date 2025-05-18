using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LivrariaApi.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public List<Book> Books { get; set; }
}