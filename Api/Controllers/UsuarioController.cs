using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcBlazor.Application.Services;
using ProcBlazor.Domain.Entities;

namespace ProcBlazor.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;


    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;

    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _usuarioService.GetAllAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _usuarioService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Add(Usuario usuario)
    {
        await _usuarioService.AddAsync(usuario);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Usuario usuario)
    {
        if (id != usuario.Id)
        {
            return BadRequest();
        }
        await _usuarioService.UpdateAsync(usuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _usuarioService.DeleteAsync(id);
        return NoContent();
    }
}
