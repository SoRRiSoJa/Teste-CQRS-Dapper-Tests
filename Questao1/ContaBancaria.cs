namespace Questao1;

public class ContaBancaria
{
    private const decimal TAXA = 3.50m;

    public int Numero { get; }
    public string Titular { get; set; }
    public decimal Saldo { get; private set; }
    public ContaBancaria(int numero, string titular, decimal saldoInicial = 0)
    {
        Numero = numero;
        Titular = titular;
        Saldo = saldoInicial;
    }
    public void Deposito(decimal valor)
    {
        Saldo += valor;
    }
    public void Saque(decimal valor)
    {
        Saldo -= valor + TAXA;
    }
    public override string ToString()
    {
        return $"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo:F2}";
    }
}
