namespace Questao5.Domain.Exceptions;

public class InvalidRequesIdException : Exception
{
    const string InvalidRequesIdMessage = "Você deve fornecer a identificação da requisição; TIPO: INVALID_REQUEST_ID";
    public InvalidRequesIdException():base(InvalidRequesIdMessage) 
    {
    }
}
