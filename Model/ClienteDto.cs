namespace ProjetoWebAPI.Model
{
    public class ClienteDto
    {
        public string Nome { get; set; }
        
        public int Telefone { get; set; }
       
        public IFormFile Documentoid { get; set; } // Campo para receber a foto
    }
}