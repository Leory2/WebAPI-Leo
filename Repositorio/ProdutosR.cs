using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDLSuperMarket.Model;
using RDLSuperMarket.ORM;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RDLSuperMarket.Repositorio
{
    public class ProdutosR
    {
        private readonly RdlsuperMarketContext _context;

        public ProdutosR(RdlsuperMarketContext context)
        {
            _context = context;
        }
        public void Add(Produto Produtos, IFormFile documentoId)
        {
            // Verifica se uma foto foi enviada
            byte[] documentoIdBytes = null;
            if (documentoId != null && documentoId.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    documentoId.CopyTo(memoryStream);
                    documentoIdBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbProduto a partir do objeto Produto recebido
            var tbProduto = new TbProduto()
            {
                Nome = Produtos.Nome,
                Preco = Produtos.Preco,
                Notaff = documentoIdBytes // Atribui a foto ao campo Notaff
            };

            // Adiciona a entidade ao contexto
            _context.TbProdutos.Add(tbProduto);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var TbProduto = _context.TbProdutos.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (TbProduto != null)
            {
                // Remove a entidade do contexto
                _context.TbProdutos.Remove(TbProduto);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }

        public List<Produto> GetAll()
        {
            List<Produto> listCli = new List<Produto>();

            var listTb = _context.TbProdutos.ToList();

            foreach (var item in listTb)
            {
                var funcionario = new Produto
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Preco = item.Preco,
                    Notaff = item.Notaff
                };

                listCli.Add(funcionario);
            }

            return listCli;
        }

        public Produto GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbProdutos.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var Produtos = new Produto
            {
                Id = item.Id,
                Nome = item.Nome,
                Preco = item.Preco,
                Notaff = item.Notaff
            };

            return Produtos; // Retorna o funcionário encontrado
        }

        public void Update(Produto Produtos, IFormFile documentoId)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var TbProduto = _context.TbProdutos.FirstOrDefault(f => f.Id == Produtos.Id);

            // Verifica se a entidade foi encontrada
            if (TbProduto != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                TbProduto.Nome = Produtos.Nome;
                TbProduto.Preco = Produtos.Preco;
                TbProduto.Notaff = Produtos.Notaff;

                // Verifica se uma nova foto foi enviada
                if (documentoId != null && documentoId.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        documentoId.CopyTo(memoryStream);
                        TbProduto.Notaff = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbProdutos.Update(TbProduto);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }

    }
}
