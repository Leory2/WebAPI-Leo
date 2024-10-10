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
    public class ClienteR
    {
        private readonly RdlsuperMarketContext _context;

        public ClienteR(RdlsuperMarketContext context)
        {
            _context = context;
        }
        public void Add(Cliente cliente, IFormFile documentoId)
        {
            // Verifica se uma foto foi enviada
            byte[] DocumentoIdBytes = null;
            if (documentoId != null && documentoId.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    documentoId.CopyTo(memoryStream);
                    DocumentoIdBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbCliente a partir do objeto Funcionario recebido
            var tbCliente = new TbCliente()
            {
                Nome = cliente.Nome,
                Telefone = cliente.Telefone, 
                Documentoid = DocumentoIdBytes // Armazena a foto na entidade
            };

            // Adiciona a entidade ao contexto
            _context.TbClientes.Add(tbCliente);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbCliente = _context.TbClientes.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbCliente != null)
            {
                // Remove a entidade do contexto
                _context.TbClientes.Remove(tbCliente);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Funcionário não encontrado.");
            }
        }
        public List<Cliente> GetAll()
        {
            List<Cliente> listCli = new List<Cliente>();

            var listTb = _context.TbClientes.ToList();

            foreach (var item in listTb)
            {
                var funcionario = new Cliente
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Telefone= item.Telefone,
                };

                listCli.Add(funcionario);
            }

            return listCli;
        }

        public Cliente GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbClientes.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var cliente = new Cliente
            {
                Id = item.Id,
                Nome = item.Nome,
                Telefone = item.Telefone,
                Documentoid = item.Documentoid,
            };

            return cliente; // Retorna o funcionário encontrado
        }

        public void Update(Cliente cliente, IFormFile documentoId)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var TbCliente = _context.TbClientes.FirstOrDefault(f => f.Id == cliente.Id);

            // Verifica se a entidade foi encontrada
            if (TbCliente != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                TbCliente.Nome = cliente.Nome;
                TbCliente.Telefone = cliente.Telefone;

                // Verifica se uma nova foto foi enviada
                if (documentoId != null && documentoId.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        documentoId.CopyTo(memoryStream);
                        TbCliente.Documentoid = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbClientes.Update(TbCliente);

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
