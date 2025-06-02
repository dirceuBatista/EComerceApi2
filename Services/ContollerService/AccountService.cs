using AutoMapper;
using LivrariaApi.Data;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace LivrariaApi.Services.ContollerService;

public class AccountService(AppDbContext context, IMapper mapper, TokenService tokenService)
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly TokenService _tokenService = tokenService;

    public async Task<ResultViewModel<UserViewModel>> AccountCreate([FromBody] RegisterViewModel model)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
        if (existingUser != null)
            return new ResultViewModel<UserViewModel>("Já existe um usuario com este email");
        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Slug = "customer",
            Roles = new List<Role>()
            
            
        };
        var password = PasswordGenerator.Generate(25);
        user.PasswordHash = PasswordHasher.Hash(password);
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
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

    public async Task<ResultViewModel<dynamic>> Login(
        [FromBody]LoginVIewModel model)
    {
        var user = await _context.Users.AsNoTracking().Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);
        
        if(user == null)
            return new ResultViewModel<dynamic>("email ou senha invalido");
        
        if(!PasswordHasher.Verify(user.PasswordHash,model.Password))
            return new ResultViewModel<dynamic>("email ou senha invalido");
        try
        {
            var token = _tokenService.GenerateToken(user);
            return new ResultViewModel<dynamic>(token);
        }
        catch (Exception e)
        {
            var innerMessage = e.InnerException?.Message ?? e.Message;
            return new ResultViewModel<dynamic>(
                $"Erro Interno - A03{e.Message}");
        }
        

    }
}