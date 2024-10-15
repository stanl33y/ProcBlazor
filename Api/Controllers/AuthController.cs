using Microsoft.AspNetCore.Mvc;
using ProcBlazor.Application.Services;
using ProcBlazor.Domain.Entities;

namespace ProcBlazor.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : Controller
{
    private readonly IUsuarioService _usuarioService;
    private readonly IJwtAuthenticationService _jwtAuthenticationService;

    public AuthController(IUsuarioService usuarioService, IJwtAuthenticationService jwtAuthenticationService)
    {
        _usuarioService = usuarioService;
        _jwtAuthenticationService = jwtAuthenticationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO) {
        var usuario = await _usuarioService.AuthenticateAsync(loginDTO.Email, loginDTO.Senha);
        
        if (usuario == null) return Unauthorized();

        var token = _jwtAuthenticationService.GenerateToken(usuario.Email, usuario.Nome, "admin");

        return Ok(new { token });
    }
}
