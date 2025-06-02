using LivrariaApi.Data;
using LivrariaApi.Extensions;
using LivrariaApi.Models;
using LivrariaApi.Services;
using LivrariaApi.Services.ContollerService;
using LivrariaApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureIdentity.Password;

namespace LivrariaApi.Controllers;

[ApiController]
public class AccountController(AccountService accountService) : ControllerBase
{
    [HttpPost("v1/accounts")]
    public async Task<IActionResult> Account(
        [FromBody]RegisterViewModel model)
    {
   
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewModel<string>(ModelState.GetErrors()));
        var create 
            = await accountService.AccountCreate(model);
        if (!create.Success)
            return Conflict(new { erros = create.Errors });
        return Ok(create.Data.Password);
    }
    
    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> Login([FromBody] LoginVIewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewModel<string>(ModelState.GetErrors()));
        var create 
            = await accountService.Login(model);
        if (!create.Success)
            return Conflict(new { erros = create.Errors });
        return Ok(create);
    }
   

}