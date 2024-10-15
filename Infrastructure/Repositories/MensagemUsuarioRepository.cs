using Dapper;
using ProcBlazor.Domain.Entities;
using ProcBlazor.Domain.Repositories;
using System.Data;

namespace ProcBlazor.Infrastructure.Repositories;

public class MensagemUsuarioRepository : IMensagemUsuarioRepository
{
    private readonly IDbConnection _connection;
    private readonly IDbTransaction _transaction;

    public MensagemUsuarioRepository(IDbConnection connection, IDbTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    public async Task<IEnumerable<MensagemUsuario>> GetAllAsync()
    {
        return await _connection.QueryAsync<MensagemUsuario>(@"
            SELECT m.*, u.Nome NomeUsuario
            FROM Mensagens m
            INNER JOIN Usuarios u ON m.IdUsuario = u.Id", transaction: _transaction);
    }

    public async Task<MensagemUsuario> GetByIdAsync(int id)
    {
        return await _connection.QueryFirstOrDefaultAsync<MensagemUsuario>(@"
            SELECT m.*, u.Nome NomeUsuario
            FROM Mensagens m
            INNER JOIN Usuarios u ON m.IdUsuario = u.Id
            WHERE Id = @Id", new { Id = id }, transaction: _transaction);
    }

    public async Task AddAsync(MensagemUsuario mensagemUsuario)
    {
        var query = "INSERT INTO Mensagens (IdUsuario, DataHora, Mensagem) VALUES (@IdUsuario, NOW(), @Mensagem)";
        await _connection.ExecuteAsync(query, mensagemUsuario, transaction: _transaction);
    }

    public async Task UpdateAsync(MensagemUsuario mensagemUsuario)
    {
        var query = "UPDATE Mensagens SET DataHora = NOW(), Mensagem = @Mensagem WHERE Id = @Id";
        await _connection.ExecuteAsync(query, mensagemUsuario, transaction: _transaction);
    }

    public async Task DeleteAsync(int id)
    {
        await _connection.ExecuteAsync("DELETE FROM Mensagens WHERE Id = @Id", new { Id = id }, transaction: _transaction);
    }

    public async Task<IEnumerable<MensagemUsuario>> GetMensagensByTextAsync(string text)
    {
        var textLike = "%" + text + "%";
        return await _connection.QueryAsync<MensagemUsuario>(@"
            SELECT m.*, u.Nome NomeUsuario
            FROM Mensagens m
            INNER JOIN Usuarios u ON m.IdUsuario = u.Id
            WHERE m.Mensagem like @Mensagem", new { Mensagem = textLike }, transaction: _transaction);
    }
}