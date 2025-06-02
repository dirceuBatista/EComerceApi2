using AutoMapper;
using LivrariaApi.Data;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Services.ContollerService;

public class BookService(AppDbContext context, IMapper mapper)
{    
private readonly AppDbContext _context = context;
private readonly IMapper _mapper = mapper;

    public  async Task<ResultViewModel<List<BookViewModel>>> GetBooks()
    {
        try
        {
            var books = await _context
                .Books
                .Include(x => x.Category)
                .ToListAsync();
            var booksDto = _mapper.Map<List<BookViewModel>>(books);
            return new ResultViewModel<List<BookViewModel>>(booksDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<List<BookViewModel>>(
                $"Erro Interno - A10{e.Message}");
        }
    }
    public async Task<ResultViewModel<BookViewModel>> GetBookById(Guid id)
    {
        try
        {
            var book = await _context
                .Books
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
                return new ResultViewModel<BookViewModel>("Livro não encontrado");
            var bookDto = _mapper.Map<BookViewModel>(book);
            return new ResultViewModel<BookViewModel>(bookDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<BookViewModel>(
                $"Erro Interno - A11{e.Message}");
        }
    }
    
    public async Task<ResultViewModel<BookViewModel>> CreateBook(BookViewModel model)
    {
        if (await _context.Books.AnyAsync(x => x.Name == model.Name))
            return new ResultViewModel<BookViewModel>("Já existe um livro com esse nome.");

        if (model.Category.Count == 0)
            return new ResultViewModel<BookViewModel>("É necessário ao menos uma categoria.");

        var book = new Book
        {
            Name = model.Name,
            Author = model.Author,
            Price = model.Price,
            Slug = model.Slug,
            Category = new List<Category>()
        };
        foreach (var item in model.Category)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == item.Name);
            if (category == null)
                return new ResultViewModel<BookViewModel>("Categoria não encontrado.");
            book.Category.Add(category); 
        }
        
        try
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            var bookDto = _mapper.Map<BookViewModel>(book);
            return new ResultViewModel<BookViewModel>(bookDto);
        }

        catch (Exception e)
        {
            return new ResultViewModel<BookViewModel>(
                $"Erro Interno - A12{e.Message}");
        }
    }
    public async Task<ResultViewModel<BookViewModel>> UpdateBook( BookViewModel model,Guid id)
    {
        var book = await _context
            .Books
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (book == null)
            return new ResultViewModel<BookViewModel>("Livro não encontrado");
        book.Name = model.Name;
        book.Author = model.Author;
        book.Slug = model.Slug;
        book.Price = model.Price;
        book.Category.Clear();
        foreach (var item in model.Category)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == item.Name);
            if (category == null)
                return new ResultViewModel<BookViewModel>($"Categoria '{item.Name}' não encontrada.");
            book.Category.Add(category);
            
        }
        try
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            var bookDto = _mapper.Map<BookViewModel>(book);
            return new ResultViewModel<BookViewModel>(bookDto);
            
        }
        catch (Exception e)
        {
            return new ResultViewModel<BookViewModel>(
                $"Erro Interno - A13{e.Message}");
        }
        
    }
    public async Task<ResultViewModel<BookViewModel>> DeleteBook(Guid id)
    {
        var book = await _context
            .Books
            .FirstOrDefaultAsync(x => x.Id == id);
        if (book == null)
            return new ResultViewModel<BookViewModel>("Livro não encontrado");
        try
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            var bookDto = _mapper.Map<BookViewModel>(book);
            return new ResultViewModel<BookViewModel>(bookDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<BookViewModel>(
                $"Erro Interno - A14{e.Message}");
        }
    }
}