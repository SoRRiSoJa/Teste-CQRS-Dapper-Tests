namespace Questao5.Domain.Exceptions;

public class InactiveAccountException : Exception
{
    const string InactiveAcoountException = "Apenas contas correntes ativas podem consultar o saldo; TIPO: INACTIVE_ACCOUNT";
    public InactiveAccountException() : base(InactiveAcoountException)
    {
    }
}
