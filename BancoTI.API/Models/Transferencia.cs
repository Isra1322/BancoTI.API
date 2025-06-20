using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Transferencia")]
public class Transferencia
{
    [Key]
    public int Numero { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Valor { get; set; }

    public string CuentaOrigen { get; set; }
    public string CuentaDestino { get; set; }

    [ForeignKey("CuentaOrigen")]
    public Cuenta CuentaOrigenNav { get; set; }

    [ForeignKey("CuentaDestino")]
    public Cuenta CuentaDestinoNav { get; set; }
}
