namespace ProcBlazor.Domain.Repositories;
public interface IUnitOfWork : IDisposable
{
    IUsuarioRepository UsuarioRepository { get; }
    IMensagemUsuarioRepository MensagemUsuarioRepository { get; }
    Task<int> CommitAsync();
}