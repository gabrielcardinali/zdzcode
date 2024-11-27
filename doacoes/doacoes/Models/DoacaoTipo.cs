namespace doacoes.Models;

public class DoacaoTipo
{
    public DoacaoTipo(int id, string descricao)
    {
        Id = id;
        Descricao = descricao;
    }

    public int Id { get; set; }
    public string Descricao { get; private set; }
}