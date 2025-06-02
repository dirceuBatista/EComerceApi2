using LivrariaApi.Extensions;
using LivrariaApi.Services.ContollerService;
using LivrariaApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaApi.Controllers;

[ApiController]
public class BookController(BookService bookService) : ControllerBase
{
    /// <summary>
    /// Retornar todos livros.
    /// </summary>
    /// <returns>Lista de livros</returns>
    [HttpGet("v1/books")]
    public async Task<IActionResult> GetBooks()
    {
        var result = await bookService.GetBooks();
        if (!result.Success)
            return StatusCode(500, new { erros = result.Errors });
        return Ok(result);
    }

    /// <summary>
    /// Retoran dados de um livro com base no Id fornecido.
    /// </summary>
    /// <param name="id do pedido em formato GUID"></param>
    /// <returns>Dados do livro correspondente</returns>
    /// <response code="200">Livro encontrado com sucesso.</response>
    /// <response code="400">ID inválido ou livro não encontrado.</response>
    [HttpGet("v1/books/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdBook([FromRoute] Guid id)
    {
        var result = await bookService.GetBookById(id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok(result.Data);
    }
    /// <summary>
    /// Cria um novo Livro
    /// </summary>
    /// <param name="livro"></param>
    /// <returns>Livro criado</returns>
    /// <response code="201">Livro criado com sucesso</response>
    /// <response code="400">Requisição inválida</response>
    /// <response code="409">Conflito (livro já existente)</response>
    [HttpPost("v1/books")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateBook(
        [FromBody] BookViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewModel<string>(ModelState.GetErrors()));
        var create 
            = await bookService.CreateBook(model);
        if (!create.Success)
            return Conflict(new { erros = create.Errors });
        return Ok(create);
    }
    /// <summary>
    /// Atualizar um livro
    /// </summary>
    /// <param name="Informações atualizadas do livro"></param>
    /// <param name="id"></param>
    /// <returns>livro atualizado</returns>
    /// /// <response code="200">Livro atualizado com sucesso</response>
    /// <response code="404">Livro não encontrado</response>
    /// <response code="400">Requisição inválida</response>
    [HttpPut("v1/books/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBook(
        [FromRoute] Guid id,
        [FromBody] BookViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        var result = await bookService.UpdateBook( model,id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok(result);
    }
    /// <summary>
    /// Deletar um livro
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Livro deletado</returns>
    ///  <response code="200">Livro deletado com sucesso</response>
    /// <response code="404">Livro não encontrado</response>
    [HttpDelete("v1/books/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
    {
        var result = await bookService.DeleteBook(id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok($"{result.Data} - Livro deletado");
    }
}
