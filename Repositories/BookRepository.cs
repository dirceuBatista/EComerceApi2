using LivrariaApi.Models;

namespace LivrariaApi.Repositories;

public class BookRepository : IBook_Repository
{
    private static List<Book> _books = new List<Book>
    {
        new Book { Id = Guid.Empty, Name = "Livro 1", Author = "Autor 1", Price = 29.99m },
        new Book { Id = Guid.Empty, Name = "Livro 2", Author = "Autor 2", Price = 39.99m },
    };
    public IEnumerable<Book> GetAllBooks()
    {
        return _bookImplementation.GetAllBooks();
    }

    public Book GetBookById(int id)
    {
        return _bookImplementation.GetBookById(id);
    }

    public void AddBook(Book book)
    {
        _bookImplementation.AddBook(book);
    }

    public void UpdateBook(Book book)
    {
        _bookImplementation.UpdateBook(book);
    }

    public void DeleteBook(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            _books.Remove(book);
        }
    }
}