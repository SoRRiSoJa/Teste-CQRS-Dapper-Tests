using Questao5.Domain.Enumerators;
using System.Globalization;

namespace Questao5.Domain.Entities;

public class Movimento
{
    public Movimento(string idContaCorrente,TipoMovimentacao tipoMovimento,decimal valor)
    {
        IdContaCorrente = idContaCorrente;
        TipoMovimento = tipoMovimento;
        Valor = valor;
    }
    public Movimento()
    {
            
    }
    public string IdMovimento { get; set; } = Guid.NewGuid().ToString();
    public string IdContaCorrente { get; set; }
    public string DataMovimento { get; set; } =  DateTime.Now.ToString(CultureInfo.InvariantCulture);
    public TipoMovimentacao TipoMovimento { get; set; }
    public decimal Valor { get; set; }
}
