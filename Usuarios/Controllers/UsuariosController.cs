using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UsuariosService.Data;
using UsuariosService.Repository;
using UsuariosService.Dto;
using UsuariosService.Models;
using UsuariosService.Profiler;

namespace UsuariosService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepo _repository;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioLerDTO>> GetUsuarios()
        {
            var todosUsuarios = _repository.SelecionarTodos();

            return Ok(_mapper.Map<IEnumerable<UsuarioLerDTO>>(todosUsuarios));
        }

        [HttpGet("{id}", Name = "GetUsuario")]
        public ActionResult<UsuarioLerDTO> GetUsuario(int id)
        {            
            var usuario = _repository.SelecionarPeloId(id);
            if (usuario!=null)
                return Ok(_mapper.Map<UsuarioLerDTO>(usuario));
            
            return NotFound();
        }

        [HttpPost]
        public ActionResult<UsuarioLerDTO> CriarUsuario(UsuarioCriarDTO usuarioCriarDTO)
        {
            var usuarioModel = _mapper.Map<Usuario>(usuarioCriarDTO);
            _repository.Criar(usuarioModel);            

            var usuarioLerDTO = _mapper.Map<UsuarioLerDTO>(usuarioModel);

            return CreatedAtRoute(nameof(GetUsuario), new { Id = usuarioLerDTO.Id }, usuarioLerDTO);

        }

        [HttpPut]
        public ActionResult<UsuarioLerDTO> AtualizarUsuario(UsuarioLerDTO usuarioLerDTO)
        {
            var usuarioModel = _mapper.Map<Usuario>(usuarioLerDTO);
            _repository.Salvar(usuarioModel);

            usuarioLerDTO = _mapper.Map<UsuarioLerDTO>(usuarioModel);

            return CreatedAtRoute(nameof(GetUsuario), new { Id = usuarioLerDTO.Id }, usuarioLerDTO);

        }

        [HttpDelete]
        public ActionResult<bool> ApagarUsuario(int id)
        {            
            _repository.Apagar(id);
            
            return Ok();

        }
    }
}
