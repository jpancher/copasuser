using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UsuariosService.Dto;
using UsuariosService.Models;


namespace UsuariosService.Profiler
{
    public class UsuarioProfiler : Profile
    {
        public UsuarioProfiler()
        {
            CreateMap<Models.Usuario, UsuarioLerDTO>();
            CreateMap<UsuarioLerDTO, Models.Usuario>();
            CreateMap<UsuarioCriarDTO, Models.Usuario>();
            CreateMap<UsuarioLerDTO, UsuarioPublishedDTO>();
        }
    }
}
