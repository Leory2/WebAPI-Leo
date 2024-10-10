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
    public class VendumR
    {
        private readonly RdlsuperMarketContext _context;

        public VendumR(RdlsuperMarketContext context)
        {
            _context = context;
        }

        public void Add(Vendum vendum, IFormFile Notafv)
        {
            // Verifica se uma foto foi enviada
            byte[] notafvBytes = null;
            if (Notafv != null && Notafv.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    Notafv.CopyTo(memoryStream);
                    notafvBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbVendum a partir do objeto Funcionario recebido
            var tbVendum = new TbVendum()
            {
                Valor = vendum.Valor,
                Notafv = vendum.Notafv,
                Quantidade = vendum.Quantidade,
                Fkcliente = vendum.Fkcliente,
                Fkproduto = vendum.Fkproduto,
            };

            // Adiciona a nova entidade ao contexto e salva as alterações
            _context.TbVendum.Add(tbVendum);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVedum = _context.TbVendum.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbVedum != null)
            {
                // Remove a entidade do contexto
                _context.TbVendum.Remove(tbVedum);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Vendum não foi encontrada.");
            }
        }
        public List<Vendum> GetAll()
        {
            List<Vendum> listVen = new List<Vendum>();

            var listTb = _context.TbVendum.ToList();

            foreach (var item in listTb)
            {
                var vendum = new Vendum
                {
                    Valor = item.Valor,
                    Notafv = item.Notafv,
                    Quantidade = item.Quantidade,
                    Fkcliente = item.Fkcliente,
                    Fkproduto = item.Fkproduto,

                };

                listVen.Add(vendum);
            }

            return listVen;
        }
        public Vendum GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbVendum.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var vendum = new Vendum
            {
                Valor = item.Valor,
                Notafv = item.Notafv,
                Quantidade = item.Quantidade,
                Fkcliente = item.Fkcliente,
                Fkproduto = item.Fkproduto,
            };

            return vendum; // Retorna o funcionário encontrado
        }
        public void Update(Vendum vendum, IFormFile notaFv)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var TbVendum = _context.TbVendum.FirstOrDefault(f => f.Id == vendum.Id);

            // Verifica se a entidade foi encontrada
            if (TbVendum != null)
            {
                TbVendum.Valor = vendum.Valor;
                TbVendum.Notafv = vendum.Notafv;
                TbVendum.Quantidade = vendum.Quantidade;
                TbVendum.Fkcliente = vendum.Fkcliente;
                TbVendum.Fkproduto = vendum.Fkproduto;
                
                // Verifica se uma nova foto foi enviada
                if (notaFv != null && notaFv.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        notaFv.CopyTo(memoryStream);
                        TbVendum.Notafv = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbVendum.Update(TbVendum);

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