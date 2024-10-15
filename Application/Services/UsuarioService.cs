using ProcBlazor.Domain.Entities;
using ProcBlazor.Domain.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace ProcBlazor.Application.Services;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario> GetByIdAsync(int id);
    Task<Usuario> GetByEmailAsync(string email);
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task DeleteAsync(int id);
    Task<Usuario> AuthenticateAsync(string email, string senha);
}

public class UsuarioService : IUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;

    public UsuarioService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync() => await _unitOfWork.UsuarioRepository.GetAllAsync();

    public async Task<Usuario> GetByIdAsync(int id) => await _unitOfWork.UsuarioRepository.GetByIdAsync(id);

    public async Task<Usuario> GetByEmailAsync(string email) => await _unitOfWork.UsuarioRepository.GetUsuarioByEmailAsync(email);
    public async Task AddAsync(Usuario usuario)
    {
        usuario.Senha = HashPassword(usuario.Senha);
        await _unitOfWork.UsuarioRepository.AddAsync(usuario);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        usuario.Senha = HashPassword(usuario.Senha);
        await _unitOfWork.UsuarioRepository.UpdateAsync(usuario);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.UsuarioRepository.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<Usuario> AuthenticateAsync(string email, string password)
    {
        var user = await _unitOfWork.UsuarioRepository.GetUsuarioByEmailAsync(email);
        if (user == null || !VerifyPassword(password, user.Senha))
        {
            return null;
        }
        return user;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }

    private bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        var hashedInput = HashPassword(inputPassword);
        return hashedInput == hashedPassword;
    }
}
