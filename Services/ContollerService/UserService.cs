using System.Collections.Frozen;
using AutoMapper;
using LivrariaApi.Data;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;
using LivrariaApi.ViewModels.InputViewModel;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Services.ContollerService;

public class UserService(AppDbContext context,IMapper mapper)
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<ResultViewModel<List<UserViewModel>>> GetUsers()
    {
        try
        {
            var users = await _context
                .Users
                .Include(x => x.customer)
                .ToListAsync();
            var usersDto = _mapper.Map<List<UserViewModel>>(users);
            return new ResultViewModel<List<UserViewModel>>(usersDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<List<UserViewModel>>(
                $"Erro Interno - A01{e.Message}");
        }
    }

    public async Task<ResultViewModel<UserViewModel>> GetUserById(Guid id)
    {
        try
        {
            var user = await _context
                .Users
                .Include(x => x.customer)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return new ResultViewModel<UserViewModel>("Usuario não encontrado");
            var userDto = _mapper.Map<UserViewModel>(user);
            return new ResultViewModel<UserViewModel>(userDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<UserViewModel>(
                $"Erro Interno - A02{e.Message}");
        }
    }

    public async Task<ResultViewModel<UserViewModel>> CreateUser(InputUserCreate model)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
        if (existingUser != null)
            return new ResultViewModel<UserViewModel>("Já existe um usuario com este email");

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            PasswordHash = model.Password,
            Slug = "Cliet",
            Roles = new List<Role>()
            
            
        };
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        var customer = new Customer
        {
            
            UserId = user.Id,  
            Name = model.Name,
            Document = model.Customer?.Document,
            Phone = model.Customer?.Phone
            
        };
        try
        {
            
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            
            var userDto = _mapper.Map<UserViewModel>(user);
            return new ResultViewModel<UserViewModel>(userDto);
        }
        catch (Exception e)
        {
            var innerMessage = e.InnerException?.Message ?? e.Message;
            return new ResultViewModel<UserViewModel>(
                $"Erro Interno - A03{e.Message}");
        }
    }

    public async Task<ResultViewModel<UserViewModel>> UpdateUser( InputUserUpdate model,Guid id)
    {
        var user = await _context
            .Users
            .Include(x => x.customer)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
            return new ResultViewModel<UserViewModel>("Usuario não encontrado");
        user.Name = model.Name;
        user.Email = model.Email;
        user.Slug = model.Slug;
        user.PasswordHash = model.PasswordHash;
        if (user.customer != null && model.Customer != null)
        {
            user.customer.Name = user.Name;
            user.customer.Phone = model.Customer?.Phone;
            user.customer.Document = model.Customer?.Document;
        }
        try
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.Entry(user.customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var userDto = _mapper.Map<UserViewModel>(user);
            return new ResultViewModel<UserViewModel>(userDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<UserViewModel>(
                $"Erro Interno - A04{e.Message}");
        }
        
    }

    public async Task<ResultViewModel<UserViewModel>> DeleteUser(Guid id)
    {
        var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
            return new ResultViewModel<UserViewModel>("Usuario não encontrado");
        try
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            var usarDto = _mapper.Map<UserViewModel>(user);
            return new ResultViewModel<UserViewModel>(usarDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<UserViewModel>(
                $"Erro Interno - A05{e.Message}");
        }
    }
}

