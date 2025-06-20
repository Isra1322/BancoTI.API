namespace BancoTI.API.DTOs
{
    public class CuentaDTO
    {
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public decimal Saldo { get; set; }
        public string CedulaCliente { get; set; }
    }
}
