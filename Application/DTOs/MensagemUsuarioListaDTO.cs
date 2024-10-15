using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcBlazor.Application.DTOs;

public class MensagemUsuarioListaDTO
{
    public int Id { get; set; }
    public string NomeUsuario { get; set; }
    public string Mensagem { get; set; }
    public DateTime DataHora { get; set; }
}
