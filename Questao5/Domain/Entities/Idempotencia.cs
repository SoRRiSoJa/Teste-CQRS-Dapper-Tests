namespace Questao5.Domain.Entities;

public class Idempotencia
{
    public Idempotencia(string chave_idempotencia, string requisicao, string resultado)
    {
        Requisicao = requisicao;
        Resultado = resultado;
        Chave_Idempotencia= chave_idempotencia;
    }
    
    public string Chave_Idempotencia { get; set; } 
    public string Requisicao { get; set; }
    public string Resultado { get; set; }
}
