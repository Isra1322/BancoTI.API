using BancoTI.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly BancoContext _context;

    public CuentasController(BancoContext context)
    {
        _context = context;
    }

    // GET: api/Cuentas?incluirCliente=true
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetCuentas([FromQuery] bool incluirCliente = false)
    {
        if (incluirCliente)
        {
            var cuentas = await _context.Cuentas
                .Include(c => c.Cliente)
                .ToListAsync();

            return cuentas.Select(c => new
            {
                c.Numero,
                c.Tipo,
                c.Saldo,
                nombreCliente = c.Cliente != null ? $"{c.Cliente.Nombre} {c.Cliente.Apellido}" : "Desconocido"
            }).ToList();
        }

        var cuentasSinCliente = await _context.Cuentas
            .Select(c => new
            {
                c.Numero,
                c.Tipo,
                c.Saldo,
                c.CedulaCliente
            }).ToListAsync();

        return cuentasSinCliente;
    }

    [HttpGet("{numero}")]
    public async Task<ActionResult<object>> GetCuenta(string numero, [FromQuery] bool incluirCliente = false)
    {
        if (incluirCliente)
        {
            var cuenta = await _context.Cuentas
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(c => c.Numero == numero);

            if (cuenta == null)
                return NotFound();

            return new
            {
                cuenta.Numero,
                cuenta.Tipo,
                cuenta.Saldo,
                nombreCliente = cuenta.Cliente != null ? $"{cuenta.Cliente.Nombre} {cuenta.Cliente.Apellido}" : "Desconocido"
            };
        }
        else
        {
            var cuenta = await _context.Cuentas
                .FirstOrDefaultAsync(c => c.Numero == numero);

            if (cuenta == null)
                return NotFound();

            return new
            {
                cuenta.Numero,
                cuenta.Tipo,
                cuenta.Saldo,
                cuenta.CedulaCliente
            };
        }
    }


    // POST: api/Cuentas
    [HttpPost]
    public async Task<IActionResult> PostCuenta([FromBody] CuentaDTO cuentaDto)
    {
        var cliente = await _context.Clientes.FindAsync(cuentaDto.CedulaCliente);
        if (cliente == null)
            return BadRequest("El cliente con esa cédula no existe.");

        var cuenta = new Cuenta
        {
            Numero = cuentaDto.Numero,
            Tipo = cuentaDto.Tipo,
            Saldo = cuentaDto.Saldo,
            CedulaCliente = cuentaDto.CedulaCliente
        };

        _context.Cuentas.Add(cuenta);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            cuenta.Numero,
            cuenta.Tipo,
            cuenta.Saldo,
            cuenta.CedulaCliente
        });
    }

    // PUT: api/Cuentas/12345
    [HttpPut("{numero}")]
    public async Task<IActionResult> ActualizarCuenta(string numero, CuentaUpdateDTO dto)
    {
        var cuenta = await _context.Cuentas.FindAsync(numero);
        if (cuenta == null)
            return NotFound();

        cuenta.Tipo = dto.Tipo;
        cuenta.Saldo = dto.Saldo;

        _context.Entry(cuenta).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Cuentas/12345
    [HttpDelete("{numero}")]
    public async Task<IActionResult> EliminarCuenta(string numero)
    {
        var cuenta = await _context.Cuentas.FindAsync(numero);
        if (cuenta == null)
            return NotFound();

        _context.Cuentas.Remove(cuenta);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
