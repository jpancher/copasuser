using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;


namespace AuditoriaService.Models
{
    public class UsuarioAudit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuditId { get; set; }
        public DateTime DataHora { get; set; }
        public string Evento { get; set; }        
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

    }
}
