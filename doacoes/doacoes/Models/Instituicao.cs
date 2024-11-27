namespace doacoes.Models;

public class Instituicao
{
    public Instituicao(int id, string nome, string responsavel, string email, string telefone, string endereco)
    {
        Id = id;
        Nome = nome;
        Responsavel = responsavel;
        Email = email;
        Telefone = telefone;
        Endereco = endereco;
    }

    public int Id { get; set; }
    public string Nome { get; private set; }
    public string Responsavel { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public string Endereco { get; private set; }
}