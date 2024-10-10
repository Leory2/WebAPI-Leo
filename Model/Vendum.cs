using RDLSuperMarket.ORM;
using System.Text.Json.Serialization;

namespace RDLSuperMarket.Model
{
    public class Vendum
    {
        public int Id { get; set; }

        public int Quantidade { get; set; }

        public decimal Valor { get; set; }

        public int Fkcliente { get; set; }

        public int Fkproduto { get; set; }

        public virtual TbCliente FkClienteNavigation { get; set; } = null!;

        public virtual TbProduto FkProdutoNavigation { get; set; } = null!;

        [JsonIgnore] // Ignora a serialização deste campo
        public byte[]? Notafv { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public string? NotafvBase64 => Notafv != null ? Convert.ToBase64String(Notafv) : null;

        public string UrlNotafv { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}