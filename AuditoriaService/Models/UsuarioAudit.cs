using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;


namespace AuditoriaService.Models
{
    public class UsuarioAudit
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Operation { get; set; }        
        public int UserId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

    }
}
