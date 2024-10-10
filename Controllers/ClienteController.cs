using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoWebAPI.Model;
using RDLSuperMarket.Model;
using RDLSuperMarket.Repositorio;


namespace RDLSuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ClienteController : ControllerBase
    {
        private readonly ClienteR _clienteRepo; // O repositório que contém GetAll()

        public object UrlDocumentoid { get; private set; }

        public ClienteController(ClienteR clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        [HttpGet("{id}/Documentoid")]
        public IActionResult GetDocumentoid(int id)
        {
            // Busca o funcionário pelo ID
            var cliente = _clienteRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (cliente == null || cliente.Documentoid == null)
            {
                return NotFound(new { Mensagem = "Documento não encontrado." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(cliente.Documentoid, "image/jpeg"); // Ou "image/png" dependendo do formato
        }
        [HttpGet]
        public ActionResult<List<Cliente>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var clientes = _clienteRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (clientes == null || !clientes.Any())
            {
                return NotFound(new { Mensagem = "Nenhum cliente encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = clientes.Select(cliente => new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                UrlDocumentoid = $"{Request.Scheme}://{Request.Host}/api/Cliente/{cliente.Id}/Documentoid" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }                        
        
        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] ClienteDto novoCliente)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var cliente = new Cliente
            {
                Nome = novoCliente.Nome,
               Telefone = novoCliente.Telefone,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _clienteRepo.Add(cliente, novoCliente.Documentoid);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ClienteDto clienteAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _clienteRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            clienteExistente.Nome = clienteAtualizado.Nome;
            clienteExistente.Telefone = clienteAtualizado.Telefone;

            // Chama o método de atualização do repositório, passando a nova foto
            _clienteRepo.Update(clienteExistente, clienteAtualizado.Documentoid);

            // Cria a URL da foto
            var urlFoto = $"{Request.Scheme}://{Request.Host}/api/Cliente/{clienteExistente.Id}/Documentoid";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",   
                Nome = clienteExistente.Nome,
                Telefone = clienteExistente.Telefone,
                UrlDocumentoid = UrlDocumentoid // Inclui a URL da foto na resposta
            };
            return Ok(resultado);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _clienteRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _clienteRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Cliente excluído com sucesso!",
                Nome = clienteExistente.Nome,
                Telefone = clienteExistente.Telefone,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
