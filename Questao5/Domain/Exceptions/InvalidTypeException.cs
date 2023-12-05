namespace Questao5.Domain.Exceptions
{
    public class InvalidTypeException : Exception
    {
        const string InvalidTypeMessage = "Apenas os tipos “débito” ou “crédito” podem ser aceitos; TIPO: INVALID_TYPE";
        public InvalidTypeException() : base(InvalidTypeMessage) 
        {
        }
    }
}
