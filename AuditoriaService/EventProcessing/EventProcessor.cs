using AuditoriaService.Dto;
using AuditoriaService.Models;
using AuditoriaService.Repository;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace AuditoriaService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly AutoMapper.IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory,
            AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.Usuario_Inserido:
                case EventType.Usuario_Atualizado:
                case EventType.Usuario_Removido:                    
                    insereUsuarioAudit(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            
            switch(eventType.Evento)
            {
                case "Usuario_Inserido":
                    Console.WriteLine("User Event Detected");
                    return EventType.Usuario_Inserido;
                case "Usuario_Atualizado":
                    Console.WriteLine("User Event Detected");
                    return EventType.Usuario_Atualizado;
                case "Usuario_Removido":
                    Console.WriteLine("User Event Detected");
                    return EventType.Usuario_Removido;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Outros;
            }
        }

        private void insereUsuarioAudit(string usuarioPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IUsuarioAuditRepo>();

                var usuarioAuditPublishedDto = JsonSerializer.Deserialize<UsuarioPublishedDTO>(usuarioPublishedMessage);

                try
                {
                    var usuarioAudit = _mapper.Map<UsuarioAudit>(usuarioAuditPublishedDto);
                    usuarioAudit.DataHora = DateTime.Now;                    
                    repo.insereUsuarioAudit(usuarioAudit);
                    Console.WriteLine($"--> Audit inserted for user id: {usuarioAudit.Id}");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not insert usuario audit to DB {ex.Message}");
                }
            }
        }
    }
}


