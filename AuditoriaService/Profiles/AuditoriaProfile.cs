using AuditoriaService.Models;
using AutoMapper;
using AuditoriaService.Dto;

namespace AuditoriaService.Profiles
{
    public class AuditoriaProfile : Profile
    {
        public AuditoriaProfile()
        {
            CreateMap<UsuarioAudit, UsuarioPublishedDTO>();
            CreateMap<UsuarioPublishedDTO, UsuarioAudit>();
        }
    }
}
