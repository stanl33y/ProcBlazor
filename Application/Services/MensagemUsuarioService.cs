using ProcBlazor.Application.DTOs;
using ProcBlazor.Domain.Entities;
using ProcBlazor.Domain.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace ProcBlazor.Application.Services;

public interface IMensagemUsuarioService
{
    Task<IEnumerable<MensagemUsuarioListaDTO>> GetAllAsync();
    Task<MensagemUsuario> GetByIdAsync(int id);
    Task AddAsync(MensagemUsuario mensagemUsuario);
    Task UpdateAsync(MensagemUsuario mensagemUsuario);
    Task DeleteAsync(int id);
    Task<IEnumerable<MensagemUsuarioListaDTO>> GetMensagensByTextAsync(string text);
}

public class MensagemUsuarioService : IMensagemUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;

    public MensagemUsuarioService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MensagemUsuarioListaDTO>> GetAllAsync()
    {
        var mensagens = await _unitOfWork.MensagemUsuarioRepository.GetAllAsync();

        return mensagens.Select(m =>
            new MensagemUsuarioListaDTO
            {
                Id = m.Id,
                Mensagem = m.Mensagem,
                DataHora = m.DataHora,
                NomeUsuario = m.NomeUsuario ?? string.Empty
            }
        );
    }

    public async Task<MensagemUsuario> GetByIdAsync(int id) => await _unitOfWork.MensagemUsuarioRepository.GetByIdAsync(id);

    public async Task AddAsync(MensagemUsuario mensagemUsuario)
    {
        await _unitOfWork.MensagemUsuarioRepository.AddAsync(mensagemUsuario);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(MensagemUsuario mensagemUsuario)
    {
        await _unitOfWork.MensagemUsuarioRepository.UpdateAsync(mensagemUsuario);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.MensagemUsuarioRepository.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<MensagemUsuarioListaDTO>> GetMensagensByTextAsync(string text)
    {
        var mensagens = await _unitOfWork.MensagemUsuarioRepository.GetMensagensByTextAsync(text);

        return mensagens.Select(m =>
            new MensagemUsuarioListaDTO
            {
                Id = m.Id,
                Mensagem = m.Mensagem,
                DataHora = m.DataHora,
                NomeUsuario = m.NomeUsuario ?? string.Empty
            }
        );
    }
}
