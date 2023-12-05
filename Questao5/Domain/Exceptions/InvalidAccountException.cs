namespace Questao5.Domain.Exceptions
{
    public class InvalidAccountException : Exception
    {
        const string InvalidAccountMessage = "Apenas contas correntes cadastradas podem consultar o saldo;TIPO: INVALID_ACCOUNT";

        public InvalidAccountException() : base(InvalidAccountMessage)
        {

        }
    }
}
