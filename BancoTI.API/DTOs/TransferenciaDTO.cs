namespace BancoTI.API.DTOs
{
    public class TransferenciaDTO
    {
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
        public string CuentaOrigen { get; set; }
        public string CuentaDestino { get; set; }
    }
}
