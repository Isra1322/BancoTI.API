namespace BancoTI.API.DTOs
{
    public class CuentaConClienteDTO
    {
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public decimal Saldo { get; set; }
        public string NombreCliente { get; set; }
    }
}
