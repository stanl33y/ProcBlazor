using Dapper;
using ProcBlazor.Domain.Entities;
using ProcBlazor.Domain.Repositories;
using System.Data;

namespace ProcBlazor.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbConnection _connection;
    private readonly IDbTransaction _transaction;

    public UsuarioRepository(IDbConnection connection, IDbTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _connection.QueryAsync<Usuario>("SELECT * FROM Usuarios", transaction: _transaction);
    }

    public async Task<Usuario> GetByIdAsync(int id)
    {
        return await _connection.QueryFirstOrDefaultAsync<Usuario>("SELECT * FROM Usuarios WHERE Id = @Id", new { Id = id }, transaction: _transaction);
    }

    public async Task AddAsync(Usuario usuario)
    {
        var query = "INSERT INTO Usuarios (Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)";
        await _connection.ExecuteAsync(query, usuario, transaction: _transaction);
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        var query = "UPDATE Usuarios SET Nome = @Nome, Email = @Email, Senha = @Senha WHERE Id = @Id";
        await _connection.ExecuteAsync(query, usuario, transaction: _transaction);
    }

    public async Task DeleteAsync(int id)
    {
        await _connection.ExecuteAsync("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id }, transaction: _transaction);
    }

    public async Task<Usuario> GetUsuarioByEmailAsync(string email)
    {
        return await _connection.QueryFirstOrDefaultAsync<Usuario>("SELECT * FROM Usuarios WHERE Email = @Email", new { Email = email }, transaction: _transaction);
    }
}