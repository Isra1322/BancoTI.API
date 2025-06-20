using BancoTI.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly BancoContext _context;

    public ClientesController(BancoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteConCuentasDTO>>> GetClientes()
    {
        return await _context.Clientes
            .Select(c => new ClienteConCuentasDTO
            {
                Cedula = c.Cedula,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Cuentas = c.Cuentas.Select(cta => cta.Numero).ToList()
            })
            .ToListAsync();
    }



    [HttpPost]
    public async Task<ActionResult<ClienteDTO>> CrearCliente(ClienteDTO dto)
    {
        var cliente = new Cliente
        {
            Cedula = dto.Cedula,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido
        };

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClientes), new { id = cliente.Cedula }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarCliente(string id, ClienteDTO dto)
    {
        if (id != dto.Cedula)
            return BadRequest("La cédula del cliente no coincide.");

        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
            return NotFound("Cliente no encontrado.");

        cliente.Nombre = dto.Nombre;
        cliente.Apellido = dto.Apellido;

        _context.Entry(cliente).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Clientes.Any(c => c.Cedula == id))
                return NotFound("Cliente no encontrado.");
            else
                throw;
        }

        return NoContent();
    }


    // DELETE: api/Clientes/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarCliente(string id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
        {
            return NotFound("Cliente no encontrado.");
        }

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
