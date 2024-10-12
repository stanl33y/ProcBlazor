namespace ProcBlazor.Domain.Repositories;
public interface IUnitOfWork : IDisposable
{
    IUsuarioRepository UsuarioRepository { get; }
    Task<int> CommitAsync();
}