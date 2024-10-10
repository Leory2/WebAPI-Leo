using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDLSuperMarket.Model;
using RDLSuperMarket.ORM;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RDLSuperMarket.Repositorio
{
    public class EnderecoR
    {
        private readonly RdlsuperMarketContext _context;

        public EnderecoR(RdlsuperMarketContext context)
        {
            _context = context;
        }

        public void Add(Endereco endereco)
        {
            // Cria uma nova entidade do tipo TbEndereco a partir do objeto Endereco recebido
            var tbEndereco = new TbEndereco()
            {
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                Numero = endereco.Numero,
                Fkcliente = endereco.Fkcliente,
                FkclienteNavigation = endereco.FkclienteNavigation,
            };

            // Adiciona a entidade ao contexto
            _context.TbEnderecos.Add(tbEndereco);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEndereco = _context.TbEnderecos.FirstOrDefault(e => e.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbEndereco != null)
            {
                // Remove a entidade do contexto
                _context.TbEnderecos.Remove(tbEndereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }

        public List<Endereco> GetAll()
        {
            List<Endereco> listEnd = new List<Endereco>();

            var listTb = _context.TbEnderecos.ToList();

            foreach (var item in listTb)
            {
                var endereco = new Endereco
                {
                    Id = item.Id,
                    Logradouro = item.Logradouro,
                    Cidade = item.Cidade,
                    Estado = item.Estado,
                    Cep = item.Cep,
                    PontoReferencia = item.PontoReferencia,
                    Numero = item.Numero,
                    Fkcliente = item.Fkcliente,
                    FkclienteNavigation = item.FkclienteNavigation,
                };

                listEnd.Add(endereco);
            }

            return listEnd;
        }

        public Endereco GetById(int id)
        {
            // Busca o endereço pelo ID no banco de dados
            var item = _context.TbEnderecos.FirstOrDefault(e => e.Id == id);

            // Verifica se o endereço foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Endereco
            var endereco = new Endereco
            {
                Id = item.Id,
                Logradouro = item.Logradouro,
                Cidade = item.Cidade,
                Estado = item.Estado,
                Cep = item.Cep,
                PontoReferencia = item.PontoReferencia,
                Numero = item.Numero,
                Fkcliente = item.Fkcliente,
                FkclienteNavigation = item.FkclienteNavigation,
            };

            return endereco; // Retorna o endereço encontrado
        }

        public void Update(Endereco endereco)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEndereco = _context.TbEnderecos.FirstOrDefault(e => e.Id == endereco.Id);

            // Verifica se a entidade foi encontrada
            if (tbEndereco != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Endereco recebido
                tbEndereco.Logradouro = endereco.Logradouro;
                tbEndereco.Cidade = endereco.Cidade;
                tbEndereco.Estado = endereco.Estado;
                tbEndereco.Cep = endereco.Cep;
                tbEndereco.PontoReferencia = endereco.PontoReferencia;
                tbEndereco.Numero = endereco.Numero;
                tbEndereco.Fkcliente = endereco.Fkcliente;

                // Atualiza as informações no contexto
                _context.TbEnderecos.Update(tbEndereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }
    }
}
