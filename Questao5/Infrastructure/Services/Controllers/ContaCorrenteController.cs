using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services.Controllers;

[ApiController]
[Route("[controller]")]
public class ContaCorrenteController : ControllerBase
{
    private readonly IMediator _mediator;
    public ContaCorrenteController(IMediator mediator)
    {
        this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Recupera o saldo da conta corrente 
    /// </summary>
    /// <param name="numeroContaCorrente">Número da conta corrente</param>
    /// <response code="400">Apenas contas correntes cadastradas podem consultar o saldo;TIPO: INVALID_ACCOUNT</response>
    /// <response code="400">Apenas contas correntes ativas podem consultar o saldo; TIPO: INACTIVE_ACCOUNT</response>
    [HttpGet("/saldo{numeroContaCorrente}")]
    [ProducesResponseType(typeof(SaldoContaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SaldoContaCorrente(int numeroContaCorrente)
    {
        try
        {
            return Ok(await _mediator.Send(new GetContaCorrenteByNumeroQuery(numeroContaCorrente)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    /// <summary>
    /// Efetua uma movimentação de credito(C) ou (D) débito em uma conta corrente 
    /// </summary>
    /// <param name="RequisicaoId">Chave de identificação da requisição</param>
    /// <param name="NumeroContaCorrente">Número da conta corrente deve ser um numero inteiro positivo maior ou igual a 3 digitos e menor que 11 digitos</param>
    /// <param name="TipoMovimentacao">Tipo da movimntação (C) crédito (D) débito</param>
    /// <param name="Valor">Valor movimentado deve ser um número positivo</param>
    /// <response code="400">"Você deve fornecer a identificação da requisição; TIPO: INVALID_REQUEST_ID</response>
    /// <response code="400">Apenas os tipos “débito” ou “crédito” podem ser aceitos; TIPO: INVALID_TYPE</response>
    /// <response code="400">Apenas valores positivos podem ser recebidos; TIPO: INVALID_VALUE</response>
    [HttpPost("/movimentar")]
    [ProducesResponseType(typeof(Movimento), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Movimentar([FromBody] CreateMovimentacaoRequestCommand createMovimentacaoRequestCommand)
    {
        try
        {
            return Ok(await _mediator.Send(createMovimentacaoRequestCommand));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
