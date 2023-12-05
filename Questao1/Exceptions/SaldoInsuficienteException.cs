using System;


namespace Questao1

{
    public class SaldoInsuficienteException : Exception
    {
        public decimal Saldo { get; }
        public decimal ValorSaque { get; }

        public SaldoInsuficienteException(decimal saldo, decimal valorSaque)
            : base("Saldo insuficiente para saque.")
        {
            Saldo = saldo;
            ValorSaque = valorSaque;
        }
    }
}
