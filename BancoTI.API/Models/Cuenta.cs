using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Cuenta")]
public class Cuenta
{
    [Key]
    public string Numero { get; set; }
    public string Tipo { get; set; }
    public decimal Saldo { get; set; }

    [ForeignKey("Cliente")]
    public string CedulaCliente { get; set; }
    public Cliente Cliente { get; set; }
}
