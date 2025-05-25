using LivrariaApi.Extensions;
using LivrariaApi.Services.ContollerService;
using LivrariaApi.ViewModels;
using LivrariaApi.ViewModels.InputViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaApi.Controllers;

[ApiController]
public class UserController(UserService userService) : ControllerBase
{
    /// <summary>
    /// Retornar todos usuarios
    /// </summary>
    /// <returns>Lista de usuarios</returns>
    [HttpGet("v1/users")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await userService.GetUsers();
        if (!result.Success)
            return StatusCode(500, new { erros = result.Errors });
        return Ok(result);
    }
    /// <summary>
    /// Retoran dados de um usuario com base no Id fornecido.
    /// </summary>
    /// <param name="id do usuario em formato GUID"></param>
    /// <returns>Dados do usuário correspondente</returns>
    /// <response code="200">Usuário encontrado com sucesso.</response>
    /// <response code="400">ID inválido ou usuário não encontrado.</response>
    [HttpGet("v1/users/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByIdUser([FromRoute] Guid id)
    {
        var result = await userService.GetUserById(id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok(result.Data);
    }
    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <param name="usuário"></param>
    /// <returns>usuário criado</returns>
    /// <response code="201">usuário criado com sucesso</response>
    /// <response code="400">Requisição inválida</response>
    /// <response code="409">Conflito (usuário já existente)</response>
    [HttpPost("v1/users")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUser(
        [FromBody] InputUserCreate model)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewModel<string>(ModelState.GetErrors()));
        var create 
            = await userService.CreateUser(model);
        if (!create.Success)
            return Conflict(new { erros = create.Errors});
        return CreatedAtAction(nameof(GetByIdUser), new { id = create.Data.Id }, create.Data);

    }
    /// <summary>
    /// Atualizar um usuário
    /// </summary>
    /// <param name="Informações atualizadas do usuário"></param>
    /// <param name="id"></param>
    /// <returns>Usuário atualizado</returns>
    /// /// <response code="200">Usuário atualizado com sucesso</response>
    /// <response code="404">Usuário não encontrado</response>
    /// <response code="400">Requisição inválida</response>
    [HttpPut("v1/users/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(
        [FromBody] InputUserUpdate model,
        [FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        var result = await userService.UpdateUser(model, id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok(result);
    }
    /// <summary>
    /// Deletar um usuario
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Usuário deletado</returns>
    ///  <response code="200">Usuário deletado com sucesso</response>
    /// <response code="404">Usuário não encontrado</response>
    [HttpDelete("v1/users/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var result = await userService.DeleteUser(id);
        if (!result.Success)
            return NotFound(new { erros = result.Errors });
        return Ok(new { message = $"{result.Data.Name} - usuário deletado" });

    }
}