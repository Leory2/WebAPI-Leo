using RDLSuperMarket.Model;
using RDLSuperMarket.ORM;
using RDLSuperMarket.Repositorio;

namespace RDLSuperMarket.Repositorio
{
    public class UsuarioRepositorio
    {
        private readonly RdlsuperMarketContext _context;

        public UsuarioRepositorio(RdlsuperMarketContext context)
        {
            _context = context;
        }

        
        
        public TbUsuario GetByCredentials(string usuario, string senha)
        {
            // Aqui você deve usar a lógica de hash para comparar a senha
            return _context.TbUsuario.FirstOrDefault(u => u.Usuario == usuario && u.Senha == senha);
        }

        // Você pode adicionar métodos adicionais para gerenciar usuários
    }
}