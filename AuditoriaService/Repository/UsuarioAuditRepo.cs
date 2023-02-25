using AuditoriaService.Data;
using AuditoriaService.Models;
using System.Threading.Tasks;

namespace AuditoriaService.Repository
{
    public class UsuarioAuditRepo : IUsuarioAuditRepo
    {
        private readonly IUsuarioAuditContext _context;

        public UsuarioAuditRepo(IUsuarioAuditContext context)
        {
            _context = context;

        }
        public async Task insereUsuarioAudit(UsuarioAudit usuarioAudit)
        {
            await _context.UsuariosAudit.InsertOneAsync(usuarioAudit);            
        }
    }
}
