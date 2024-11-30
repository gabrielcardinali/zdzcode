namespace doacoes.Models;

public class Doacao
{
    public Doacao(int id, int doadorId, int instituicaoId, int doacaoTipoId, int? valor, int? quantidade,
        string descricao)
    {
        Id = id;
        DoadorId = doadorId;
        InstituicaoId = instituicaoId;
        DoacaoTipoId = doacaoTipoId;
        Valor = valor;
        Quantidade = quantidade;
        Descricao = descricao;
    }

    protected Doacao()
    {
    }

    public int Id { get; set; }
    public int DoadorId { get; private set; }
    public Doador Doador { get; set; }
    public int InstituicaoId { get; private set; }
    public Instituicao Instituicao { get; set; }
    public int DoacaoTipoId { get; private set; }
    public DoacaoTipo DoacaoTipo { get; set; }
    public DateTime Data { get; private set; } = DateTime.Now;
    public int? Valor { get; private set; }
    public int? Quantidade { get; private set; }
    public string Descricao { get; private set; }
}