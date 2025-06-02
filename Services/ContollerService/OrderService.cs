using AutoMapper;
using LivrariaApi.Data;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;
using LivrariaApi.ViewModels.InputOrder;
using Microsoft.EntityFrameworkCore;

namespace LivrariaApi.Services.ContollerService;

public class OrderService(AppDbContext context, IMapper mapper)
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ResultViewModel<OrderViewModel>> CreateOrder(InputOrderCreate model)
    {
        var bookIds = model.OrderItems.Select(i => i.BookId).ToList();
        var books = await _context.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();

        if (books.Count != bookIds.Count)
            return new ResultViewModel<OrderViewModel>("Um ou mais livros não foram encontrados.");
        
        var order = new Order
        {
            CustomerId = model.CustomerId,
            OrderItems = new List<OrderItem>()
        };
        foreach (var item in model.OrderItems)
        {
            var book = books.FirstOrDefault(b => b.Id == item.BookId); 
            if (book == null)
                return new ResultViewModel<OrderViewModel>("Livro não encontrado.");

            var orderItem = new OrderItem
            {
                BookId = book.Id,
                BookName = book.Name,
                Quantity = item.Quantity,
                UnitPrice = book.Price,
                Total = book.Price * item.Quantity
            };
            order.OrderItems.Add(orderItem); 
        }
        try
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            var orderDto = _mapper.Map<OrderViewModel>(order);
            return new ResultViewModel<OrderViewModel>(orderDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<OrderViewModel>(
                $"Erro Interno - A06{e.Message}");
        }
    }

    public async Task<ResultViewModel<List<OrderViewModel>>>GetOrders()
    {
        try
        {
            var orders =
                await _context.Orders.Include(x => x.OrderItems).ToListAsync();
            var ordersDto = _mapper.Map<List<OrderViewModel>>(orders);
            return new ResultViewModel<List<OrderViewModel>>(ordersDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<List<OrderViewModel>>(
                $"Erro Interno - A06{e.Message}");
        }
        
    }
    public async Task<ResultViewModel<OrderViewModel>> GetOrderById(Guid id)
    {
        try
        {
            var order = await _context
                .Orders
                .Include(x=>x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return new ResultViewModel<OrderViewModel>(
                    "Pedido não encontrado");
            var orderDto = _mapper.Map<OrderViewModel>(order);
            return new ResultViewModel<OrderViewModel>(orderDto);
        }
        catch (Exception e)
        {

            return new ResultViewModel<OrderViewModel>(
                $"Erro Interno - A07{e.Message}");
        }
    }

   public async Task<ResultViewModel<OrderViewModel>> UpdateOrder( Guid id, InputOrderUpdate model)
    {
        var order =
            await _context
                .Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == id);
        if (order == null)
            return new ResultViewModel<OrderViewModel>(
                "Pedido não encontrado");
        order.CustomerId = model.CustomerId;
        if (order.OrderItems != null && model.OrderItems != null)
        {
            _context.OrderItems.RemoveRange(order.OrderItems);

            order.OrderItems = model.OrderItems.Select(item => new OrderItem
            {
                BookId = item.BookId,
                BookName = item.BookName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Total = item.Quantity * item.UnitPrice,
                OrderId = order.Id
            }).ToList();
        }
        try
        {
            await _context.SaveChangesAsync();
            var orderDto = _mapper.Map<OrderViewModel>(order);
            return new ResultViewModel<OrderViewModel>(orderDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<OrderViewModel>(
                $"Erro Interno - A08{e.Message}");
        }

        
    }
    public async Task<ResultViewModel<OrderViewModel>> DeleteOrder(Guid id)
    {
        var order = await _context
            .Orders
            .Include(x=>x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (order == null)
            return new ResultViewModel<OrderViewModel>("pedido não encontrado");
        try
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            var orderDto = _mapper.Map<OrderViewModel>(order);
            return new ResultViewModel<OrderViewModel>(orderDto);
        }
        catch (Exception e)
        {
            return new ResultViewModel<OrderViewModel>(
                $"Erro Interno - A09{e.Message}");
        }
    }


}