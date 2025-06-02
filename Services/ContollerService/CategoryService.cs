using AutoMapper;
using LivrariaApi.Data;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Services.ContollerService;

public class CategoryService(AppDbContext context, IMapper mapper)
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public  async Task<ResultViewModel<List<CategoryViewModel>>> GetCategory()
    {
        try
        {
            var categories = await _context
                .Categories
                .Include(x => x.Books)
                .ToListAsync();
            var categoriesDto = _mapper.Map<List<CategoryViewModel>>(categories);
            return new ResultViewModel<List<CategoryViewModel>>(categoriesDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<List<CategoryViewModel>>(
                $"Erro Interno - A15{e.Message}");
        }
    }
    public async Task<ResultViewModel<CategoryViewModel>> GetCategoryById(Guid id)
    {
        try
        {
            var category = await _context
                .Categories
                .Include(x => x.Books)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return new ResultViewModel<CategoryViewModel>("Categoria não encontrado");
            var categoryDto = _mapper.Map<CategoryViewModel>(category);
            return new ResultViewModel<CategoryViewModel>(categoryDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<CategoryViewModel>(
                $"Erro Interno - A16{e.Message}");
        }
    }
    public async Task<ResultViewModel<CategoryViewModel>> CreateCategory(CategoryViewModel model)
    {
        var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Name == model.Name);
        if (existingCategory != null)
            return new ResultViewModel<CategoryViewModel>("Já existe esta categoria ");

        var category = new Category()
        {
            Name = model.Name,
            Slug = model.Slug,
            Books = new List<Book>()
        };
        if (true)
        {
            foreach (var item in model.Books)
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Name == item.Name);
                if (book == null)
                    return new ResultViewModel<CategoryViewModel>($"Livro '{item.Name}' não encontrado.");
                category.Books.Add(book);
            }
        }
        try
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            var categoryDto = _mapper.Map<CategoryViewModel>(category);
            return new ResultViewModel<CategoryViewModel>(categoryDto);
        }

        catch (Exception e)
        {
            return new ResultViewModel<CategoryViewModel>(
                $"Erro Interno - A17{e.Message}");
        }
    }

    public async Task<ResultViewModel<CategoryViewModel>> UpdateCategory(CategoryViewModel model, Guid id)
    {
        var category = await _context
            .Categories
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return new ResultViewModel<CategoryViewModel>("Categoria não encontrado");
        category.Name = model.Name;
        category.Slug = model.Slug;
        category.Books.Clear();
        if (model.Books != null)
        {
            foreach (var item in model.Books)
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Name == item.Name);
                if (book == null)
                    return new ResultViewModel<CategoryViewModel>($"Livro '{item.Id}' não encontrado.");
                category.Books.Add(book);
            }
        }
        try
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            var categoryDto = _mapper.Map<CategoryViewModel>(category);
            return new ResultViewModel<CategoryViewModel>(categoryDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<CategoryViewModel>(
                $"Erro Interno - A18{e.Message}");
        }
    }
    public async Task<ResultViewModel<CategoryViewModel>> DeleteCategory(Guid id)
    {
        var category = await _context
            .Categories
            .FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return new ResultViewModel<CategoryViewModel>("Categoria não encontrado");
        try
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            var categoryDto = _mapper.Map<CategoryViewModel>(category);
            return new ResultViewModel<CategoryViewModel>(categoryDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<CategoryViewModel>(
                $"Erro Interno - A19{e.Message}");
        }
    }

}