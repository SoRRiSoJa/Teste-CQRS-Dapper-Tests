namespace Questao5.Application.Queries.Responses;

public class SaldoContaResponse
{
    public SaldoContaResponse(string titular, long numeroContaCorrente, decimal saldo)
    {
        NumeroConta = numeroContaCorrente;
        Titular = titular;
        Saldo = saldo;
        DataRepososta = DateTime.Now;
    }
    public long NumeroConta { get; set; }
    public string Titular { get; set; }
    public DateTime DataRepososta { get; set; }
    public decimal Saldo { get; set; }
}
