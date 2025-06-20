using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Cliente")]
public class Cliente
{
    [Key]
    public string Cedula { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }

    public ICollection<Cuenta> Cuentas { get; set; }
}
