using AuditoriaService.Models;
using System.Threading.Tasks;

namespace AuditoriaService.Repository
{
    public interface IUsuarioAuditRepo
    {
        Task insereUsuarioAudit(UsuarioAudit usuarioAudit);
    }
}
