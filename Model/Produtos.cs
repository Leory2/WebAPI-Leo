

using System.Text.Json.Serialization;

namespace RDLSuperMarket.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public byte[]? Notaff { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public string? FotoBase64 => Notaff != null ? Convert.ToBase64String(Notaff) : null;

        public string UrlNotaff { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}
