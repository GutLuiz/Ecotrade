namespace backend.Models
{
    public class Transacao
    {
        public int CompradorId { get; set; }
        public int VendedorId { get; set; }
        public int CreditoId { get; set; }
        public decimal Quantidade { get; set; }
    }
}
