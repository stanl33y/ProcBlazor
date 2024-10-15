namespace ProcBlazor.Domain.Entities;

public class MensagemUsuario
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public string? NomeUsuario { get; set; }
    public DateTime DataHora { get; set; }
    public required string Mensagem { get; set; }
}
