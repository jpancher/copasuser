using AuditoriaService.Models;
using MongoDB.Driver;

namespace AuditoriaService.Data
{
    public interface IUsuarioAuditContext
    {
        IMongoCollection<UsuarioAudit> UsuariosAudit { get; }
    }
}
