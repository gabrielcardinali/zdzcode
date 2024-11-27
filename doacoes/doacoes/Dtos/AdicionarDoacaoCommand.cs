using System.Security.Principal;
using doacoes.Models;

namespace doacoes.Dtos;

public class AdicionarDoacaoCommand
{
    public int Id { get; set; }
    public int DoadorId { get; set; }
    public int InstituicaoId { get; set; }
    public int DoacaoTipoId { get; set; }
    public int? Valor { get; set; }
    public int? Quantidade { get; set; }
    public string Descricao { get; set; }
}