using LivrariaApi.Extensions;
using LivrariaApi.Services.ContollerService;
using LivrariaApi.ViewModels;
using LivrariaApi.ViewModels.InputOrder;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaApi.Controllers;

public class OrderController(OrderService orderService) : ControllerBase
{
    /// <summary>
    /// Retornar todos pedidos.
    /// </summary>
    /// <returns>Lista de pedidos</returns>
    [HttpGet("v1/orders")]
    public async Task<IActionResult> GetOrders()
    {
        var result = await orderService.GetOrders();
        if (!result.Success)
            return StatusCode(500, new { erros = result.Errors });
        return Ok(result);
    }
    /// <summary>
    /// Retoran dados de um pedido com base no Id fornecido.
    /// </summary>
    /// <param name="id do pedido em formato GUID"></param>
    /// <returns>Dados do pedido correspondente</returns>
    /// <response code="200">pedido encontrado com sucesso.</response>
    /// <response code="400">ID inválido ou pedido não encontrado.</response>
    [HttpGet("v1/orders/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdOrder([FromRoute] Guid id)
    {
        var result = await orderService.GetOrderById(id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok(result.Data);
    }
    /// <summary>
    /// Cria um novo pedido
    /// </summary>
    /// <param name="pedido"></param>
    /// <returns>usuário criado</returns>
    /// <response code="201">pedido criado com sucesso</response>
    /// <response code="400">Requisição inválida</response>
    /// <response code="409">Conflito (pedido já existente)</response>
    [HttpPost("v1/orders")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateOrder(
        [FromBody] InputOrderCreate model)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewModel<string>(ModelState.GetErrors()));
        var create 
            = await orderService.CreateOrder(model);
        if (!create.Success)
            return Conflict(new { erros = create.Errors });
        return Created($"/api/orders/{create.Data}", create.Data);

    }
   /// <summary>
    /// Atualizar um pedido
    /// </summary>
    /// <param name="Informações atualizadas do pedido"></param>
    /// <param name="id"></param>
    /// <returns>Pedido atualizado</returns>
    /// /// <response code="200">Pedido atualizado com sucesso</response>
    /// <response code="404">Pedido não encontrado</response>
    /// <response code="400">Requisição inválida</response>
    [HttpPut("v1/orders/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateOrder(
        [FromRoute] Guid id,
        [FromBody] InputOrderUpdate model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        var result = await orderService.UpdateOrder( id,model);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok(result);
    }
    /// <summary>
    /// Deletar um pedido
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Pedido deletado</returns>
    ///  <response code="200">Pedido deletado com sucesso</response>
    /// <response code="404">Pedido não encontrado</response>
    [HttpDelete("v1/orders/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
    {
        var result = await orderService.DeleteOrder(id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok($"{result.Data} - pedido deletado");
    }
}