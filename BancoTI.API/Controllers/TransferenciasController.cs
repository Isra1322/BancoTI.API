using BancoTI.API.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TransferenciasController : ControllerBase
{
    private readonly BancoContext _context;

    public TransferenciasController(BancoContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostTransferencia([FromBody] TransferenciaDTO dto)
    {
        // Validar si las cuentas existen
        var cuentaOrigen = await _context.Cuentas.FindAsync(dto.CuentaOrigen);
        var cuentaDestino = await _context.Cuentas.FindAsync(dto.CuentaDestino);

        if (cuentaOrigen == null || cuentaDestino == null)
            return BadRequest("Una o ambas cuentas no existen.");

        if (cuentaOrigen.Saldo < dto.Valor)
            return BadRequest("Saldo insuficiente en la cuenta origen.");

        // Actualizar saldos
        cuentaOrigen.Saldo -= dto.Valor;
        cuentaDestino.Saldo += dto.Valor;

        // Crear la transferencia
        var transferencia = new Transferencia
        {
            Fecha = DateTime.Now, // Forzar fecha actual
            Valor = dto.Valor,
            CuentaOrigen = dto.CuentaOrigen,
            CuentaDestino = dto.CuentaDestino
        };


        _context.Transferencias.Add(transferencia);
        await _context.SaveChangesAsync();

        return Ok(transferencia);
    }
}
