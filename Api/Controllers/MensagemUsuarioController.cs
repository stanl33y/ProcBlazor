using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcBlazor.Application.Services;
using ProcBlazor.Domain.Entities;
using System.Security.Claims;

namespace ProcBlazor.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class MensagemUsuarioController : Controller
{
    private readonly IMensagemUsuarioService _mensagemUsuarioService;
    private readonly IUsuarioService _usuarioService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MensagemUsuarioController(IMensagemUsuarioService mensagemUsuarioService, IUsuarioService usuarioService, IHttpContextAccessor httpContextAccessor)
    {
        _mensagemUsuarioService = mensagemUsuarioService;
        _usuarioService = usuarioService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _mensagemUsuarioService.GetAllAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var mensagem = await _mensagemUsuarioService.GetByIdAsync(id);
        if (mensagem == null)
        {
            return NotFound();
        }
        return Ok(mensagem);
    }

    [HttpPost]
    public async Task<IActionResult> Add(MensagemUsuario mensagemUsuario)
    {
        string email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Erro ao buscar email do usuario.");
        }

        var usuario = await _usuarioService.GetByEmailAsync(email);

        if (usuario == null)
        {
            return BadRequest("Erro ao buscar usuario");
        }

        mensagemUsuario.IdUsuario = usuario.Id;

        await _mensagemUsuarioService.AddAsync(mensagemUsuario);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MensagemUsuario mensagemUsuario)
    {

        string email = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Erro ao buscar email do usuario.");
        }

        var usuario = await _usuarioService.GetByEmailAsync(email);

        if (usuario == null)
        {
            return BadRequest("Erro ao buscar usuario");
        }

        mensagemUsuario.IdUsuario = usuario.Id;

        if (id != mensagemUsuario.IdUsuario)
        {
            return BadRequest();
        }
        await _mensagemUsuarioService.UpdateAsync(mensagemUsuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mensagemUsuarioService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("BuscarPorTexto")]
    public async Task<IActionResult> GetByText([FromQuery]string text)
    {
        var mensagens = await _mensagemUsuarioService.GetMensagensByTextAsync(text);
        if (mensagens == null)
        {
            return NotFound();
        }
        return Ok(mensagens);
    }
}
