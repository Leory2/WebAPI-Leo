using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDLSuperMarket.Repositorio;
using RDLSuperMarket.Model;
using RDLSuperMarket.ORM;
using Microsoft.AspNetCore.Authorization;

namespace RDLSuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VendumController : ControllerBase
    {
        private readonly VendumR _vendumRepo; // O repositório que contém GetAll()

        public object UrlDocumentoid { get; set; }

        public VendumController(VendumR vendumRepo)
        {
            _vendumRepo = vendumRepo;
        }
        [HttpGet("{id}/Notafv")]
        public IActionResult GetNotafv(int id)
        {
            // Busca o funcionário pelo ID
            var vendum = _vendumRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendum == null || vendum.Notafv == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrado." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(vendum.Notafv, "image/jpeg"); // Ou "image/png" dependendo do formato
        }
        [HttpGet]
        public ActionResult<List<Vendum>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var vendums = _vendumRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (vendums == null || !vendums.Any())
            {
                return NotFound(new { Mensagem = "Nenhuma venda encontrada." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = vendums.Select(vendum => new Vendum
            {
                Id = vendum.Id,
                Valor = vendum.Valor,
                Notafv = vendum.Notafv,
                Quantidade = vendum.Quantidade,
                Fkcliente = vendum.Fkcliente,
                Fkproduto = vendum.Fkproduto,
                UrlNotafv = $"{Request.Scheme}://{Request.Host}/api/Vendum/{vendum.Id}/Notafv" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        [HttpPost]
        public ActionResult<object> Post([FromForm] VendumDto novaVendum)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var vendum = new Vendum
            {
                Valor = novaVendum.Valor,
                Quantidade = novaVendum.Quantidade,
                Fkcliente = novaVendum.Fkcliente,
                Fkproduto = novaVendum.Fkproduto,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _vendumRepo.Add(vendum, novaVendum.Notafv);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usu cadastrado com sucesso!",
                Valor = vendum.Valor,
                Quantidade = vendum.Quantidade,
                Fkcliente = novaVendum.Fkcliente,
                Fkproduto = novaVendum.Fkproduto,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);

        }
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] VendumDto vendumAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var vendumExistente = _vendumRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendumExistente == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            vendumExistente.Valor = vendumAtualizado.Valor;
            vendumExistente.Quantidade = vendumAtualizado.Quantidade;
            vendumExistente.Fkproduto = vendumExistente.Fkproduto;
            vendumExistente.Fkcliente = vendumExistente.Fkcliente;

            // Chama o método de atualização do repositório, passando a nova foto
            _vendumRepo.Update(vendumExistente, vendumAtualizado.Notafv);

            // Cria a URL da foto
            var urlFoto = $"{Request.Scheme}://{Request.Host}/api/Vendum/{vendumExistente.Id}/Notafv";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Valor = vendumExistente.Valor,
                Quantidade = vendumExistente.Quantidade,
                UrlNotafv = vendumExistente.Notafv,// Inclui a URL da foto na resposta
                Fkproduto = vendumExistente.Fkcliente,
                Fkcliente = vendumExistente.Fkcliente,
            };
            return Ok(resultado);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var vendumExistente = _vendumRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendumExistente == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." });
            }

            // Chama o método de exclusão do repositório
            _vendumRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Cliente excluído com sucesso!",
                Valor = vendumExistente.Valor,
                Quantidade = vendumExistente.Quantidade,
                UrlNotafv = vendumExistente.Notafv,// Inclui a URL da foto na resposta
                Fkproduto = vendumExistente.Fkcliente,
                Fkcliente = vendumExistente.Fkcliente,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
} 
  
