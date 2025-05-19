using System.Collections.Frozen;
using AutoMapper;
using LivrariaApi.Data;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Services.ContollerService;

public class UserService(AppDbContext context, IMapper mapper)
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

    public async Task<ResultViewModel<UserViewModel>> CreateUser(UserViewModel model)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
        if (existingUser != null)
            return new ResultViewModel<UserViewModel>("Já existe um usuario com este email");

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Slug,
            Roles = new List<Role>(),
            customer = new Customer()
            {
                UserId = model.Id,
                Name = model.Name
            }
        };
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var userDto = _mapper.Map<UserViewModel>(user);
            return new ResultViewModel<UserViewModel>(userDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<UserViewModel>(
                $"Erro Interno - A03{e.Message}");
        }
    }

    public async Task<ResultViewModel<UserViewModel>> UpdateUser( UserViewModel model,Guid id)
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
        user.customer.UserId = user.Id;
        user.customer.Name = user.Name;

        try
        {
            _context.Users.Update(user);
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

