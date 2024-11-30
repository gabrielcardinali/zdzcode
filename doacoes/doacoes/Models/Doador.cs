namespace doacoes.Models;

public class Doador
{
    public Doador(int id, string nome, string email, string telefone, string endereco)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Endereco = endereco;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    private readonly List<Doacao> _doacoes = new();
    public IReadOnlyCollection<Doacao> Doacao => _doacoes;
}