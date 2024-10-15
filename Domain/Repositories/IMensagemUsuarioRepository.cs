using ProcBlazor.Domain.Entities;

namespace ProcBlazor.Domain.Repositories;
public interface IMensagemUsuarioRepository : IRepository<MensagemUsuario>
{
    Task<IEnumerable<MensagemUsuario>> GetMensagensByTextAsync(string text);
}
