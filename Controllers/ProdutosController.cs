using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoWebAPI.Model;
using RDLSuperMarket.Model;
using RDLSuperMarket.Repositorio;

namespace RDLSuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutosR _produtosRepo; // O repositório que contém GetAll()

        public ProdutosController(ProdutosR produtosRepo)
        {
            _produtosRepo = produtosRepo;
        }

        [HttpGet("{id}/Notaff")]
        public IActionResult GetDocumentoid(int id)
        {
            // Busca o produto pelo ID
            var produto = _produtosRepo.GetById(id);

            // Verifica se o produto foi encontrado
            if (produto == null || produto.Notaff == null)
            {
                return NotFound(new { Mensagem = "Nota fiscal não encontrada." });
            }

            // Retorna a nota fiscal como um arquivo
            return File(produto.Notaff, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        [HttpGet]
        public ActionResult<List<Produto>> GetAll()
        {
            // Chama o repositório para obter todos os produtos
            var produtos = _produtosRepo.GetAll();

            // Verifica se a lista de produtos está vazia
            if (produtos == null || !produtos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum produto encontrado." });
            }

            // Retorna a lista de produtos com status 200 OK
            return Ok(produtos);
        }

        [HttpPost]
        public ActionResult<object> Post([FromForm] ProdutosDto novoProduto)
        {
            // Cria uma nova instância do modelo Produto a partir do DTO recebido
            var produto = new Produto
            {
                Nome = novoProduto.Nome,
                Preco = novoProduto.Preco,
            };

            // Chama o método de adicionar do repositório
            _produtosRepo.Add(produto, novoProduto.Notaff);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto cadastrado com sucesso!",
                Nome = produto.Nome,
                Preco = produto.Preco,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ProdutosDto produtoAtualizado)
        {
            // Busca o produto existente pelo Id
            var produtoExistente = _produtosRepo.GetById(id);

            // Verifica se o produto foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Atualiza os dados do produto existente com os valores do objeto recebido
            produtoExistente.Nome = produtoAtualizado.Nome;
            produtoExistente.Preco = produtoAtualizado.Preco;

            // Chama o método de atualização do repositório
            _produtosRepo.Update(produtoExistente, produtoAtualizado.Notaff);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto atualizado com sucesso!",
                Nome = produtoExistente.Nome,
                Preco = produtoExistente.Preco,
            };
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o produto existente pelo Id
            var produtoExistente = _produtosRepo.GetById(id);

            // Verifica se o produto foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _produtosRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto excluído com sucesso!",
                Nome = produtoExistente.Nome,
                Preco = produtoExistente.Preco,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}
