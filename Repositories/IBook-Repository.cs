using LivrariaApi.Models;

namespace LivrariaApi.Repositories;

public interface IBook_Repository
{
    IEnumerable<Book> GetAllBooks();
    Book GetBookById(int id);
    void AddBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(int id);
}