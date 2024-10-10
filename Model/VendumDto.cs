namespace RDLSuperMarket.Model
{
    public class VendumDto
    {
        public int Quantidade { get; set; }

        public decimal Valor { get; set; }

        public int Fkcliente { get; set; }

        public int Fkproduto { get; set; }

        public IFormFile Notafv { get; set; }

    }
}
