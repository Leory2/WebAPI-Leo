using RDLSuperMarket.ORM;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RDLSuperMarket.Model
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public int Telefone { get; set; }

        [JsonIgnore]
        public byte[]? Documentoid { get; set; } 
       
        [JsonIgnore] // Ignora a serialização deste campo
        public string? DocumentoidBase64 => Documentoid != null ? Convert.ToBase64String(Documentoid) : null;

        public string UrlDocumentoid { get; set; }
    }

}