namespace BancoTI.API.DTOs
{
    public class ClienteConCuentasDTO
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public List<string> Cuentas { get; set; }
    }
}
