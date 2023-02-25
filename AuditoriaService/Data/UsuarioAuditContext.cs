using AuditoriaService.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AuditoriaService.Data
{
    public class UsuarioAuditContext : IUsuarioAuditContext
    {
        public UsuarioAuditContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            UsuariosAudit = database.GetCollection<UsuarioAudit>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }
        public IMongoCollection<UsuarioAudit> UsuariosAudit { get; }
    }
}
