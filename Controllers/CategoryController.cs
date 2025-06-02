using LivrariaApi.Extensions;
using LivrariaApi.Services.ContollerService;
using LivrariaApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace LivrariaApi.Controllers;
[ApiController]
public class CategoryController(CategoryService categoryService): ControllerBase
{
    /// <summary>
    /// Retornar todos livros.
    /// </summary>
    /// <returns>Lista de livros</returns>
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetCategories()
    {
        
        var result = await categoryService.GetCategory();
        if (!result.Success)
            return StatusCode(500, new { erros = result.Errors });
        return Ok(result);
    }
    /// <summary>
    /// Retoran dados de uma categoria com base no Id fornecido.
    /// </summary>
    /// <param name="id do pedido em formato GUID"></param>
    /// <returns>Dados da categoria correspondente</returns>
    /// <response code="200">Categoria encontrado com sucesso.</response>
    /// <response code="400">ID inválido ou categoria não encontrado.</response>
    [HttpGet("v1/categories/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
    {
        var result = await categoryService.GetCategoryById(id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok(result.Data);
    }
    /// <summary>
    /// Cria uma nova categoria de livros
    /// </summary>
    /// <param name="categoria "></param>
    /// <returns>categoria criada</returns>
    /// <response code="201">Categoria criada com sucesso</response>
    /// <response code="400">Requisição inválida</response>
    /// <response code="409">Conflito (livro já existente)</response>
    [HttpPost("v1/categories")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateCategory(
        [FromBody] CategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewModel<string>(ModelState.GetErrors()));
        var create 
            = await categoryService.CreateCategory(model);
        if (!create.Success)
            return Conflict(new { erros = create.Errors });
        return Ok(create);
    }
    /// <summary>
    /// Atualizar uma categoria
    /// </summary>
    /// <param name="Informações atualizadas da categoria"></param>
    /// <param name="id"></param>
    /// <returns>categoria atualizado</returns>
    /// /// <response code="200">categoria atualizado com sucesso</response>
    /// <response code="404">Categoria não encontrado</response>
    /// <response code="400">Requisição inválida</response>
    [HttpPut("v1/categories/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCategory(
        [FromRoute] Guid id,
        [FromBody] CategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        var result = await categoryService.UpdateCategory( model,id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok(result);
    }
    /// <summary>
    /// Deletar uma categoria
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Categoria deletada</returns>
    ///  <response code="200">Categoria deletada com sucesso</response>
    /// <response code="404">Categoria não encontrada</response>
    [HttpDelete("v1/categories/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
    {
        var result = await categoryService.DeleteCategory(id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok($"{result.Data} - Categoria deletada");
    }
}