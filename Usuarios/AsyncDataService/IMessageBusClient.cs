using UsuariosService.Dto;

namespace UsuariosService.AsyncDataService
{
    public interface IMessageBusClient
    {
        void PublishNewEvent(UsuarioPublishedDTO usuarioPublishedDto);
    }
}
