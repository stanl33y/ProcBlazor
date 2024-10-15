using MySql.Data.MySqlClient;
using ProcBlazor.Domain.Repositories;
using ProcBlazor.Infrastructure.Repositories;
using System.Data;

namespace ProcBlazor.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction;
    private IUsuarioRepository _usuarioRepository;
    private IMensagemUsuarioRepository _mensagemUsuarioRepository;
    private bool _disposed;

    public UnitOfWork(string connectionString)
    {
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    public IUsuarioRepository UsuarioRepository => _usuarioRepository ??= new UsuarioRepository(_connection, _transaction);
    public IMensagemUsuarioRepository MensagemUsuarioRepository => _mensagemUsuarioRepository ??= new MensagemUsuarioRepository(_connection, _transaction);

    public async Task<int> CommitAsync()
    {
        try
        {
            _transaction.Commit();
            return 1;
        }
        catch
        {
            _transaction.Rollback();
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _transaction = _connection.BeginTransaction();
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _transaction?.Dispose();
            _connection?.Dispose();
            _disposed = true;
        }
    }
}