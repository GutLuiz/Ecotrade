namespace backend.Models
{
    public class CreditoCarbono
    {
        public int ProdutorId { get; set; }
        public decimal Quantidade { get; set; }
        public string Origem { get; set; }
        public DateTime DataGeracao { get; set; }

        public string Status { get; set; } = "pendente";
    }
}
