namespace Questao5.Domain.Exceptions
{
    public class InvalidValueException : Exception
    {
        const string InvalidValueMessage = "Apenas valores positivos podem ser recebidos; TIPO: INVALID_VALUE";
        public InvalidValueException() : base(InvalidValueMessage) 
        {
        }
    }
}
