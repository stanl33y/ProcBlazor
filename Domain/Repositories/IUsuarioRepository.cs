using ProcBlazor.Domain.Entities;

namespace ProcBlazor.Domain.Repositories;
public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario> GetUsuarioByEmailAsync(string email);
}
