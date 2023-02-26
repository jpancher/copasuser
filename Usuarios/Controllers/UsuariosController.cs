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
using UsuariosService.AsyncDataService;
using Microsoft.AspNetCore.Authorization;

namespace UsuariosService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepo _repository;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public UsuariosController(
            IUsuarioRepo repository, 
            IMapper mapper,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _messageBusClient = messageBusClient;

        }

        [HttpGet]
        public IActionResult GetUsuarios()
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
        public IActionResult CriarUsuario(UsuarioCriarDTO usuarioCriarDTO)
        {
            var usuarioModel = _mapper.Map<Usuario>(usuarioCriarDTO);
            _repository.Criar(usuarioModel);            

            var usuarioLerDTO = _mapper.Map<UsuarioLerDTO>(usuarioModel);

            //send async message
            try
            {
                var usuarioPublishedDTO = _mapper.Map<UsuarioPublishedDTO>(usuarioLerDTO);
                usuarioPublishedDTO.Evento = "Usuario_Inserido";
                _messageBusClient.PublishNewEvent(usuarioPublishedDTO);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetUsuario), new { Id = usuarioLerDTO.Id }, usuarioLerDTO);

        }
        
        [HttpPut]
        public IActionResult AtualizarUsuario(UsuarioLerDTO usuarioLerDTO)
        {
            var usuarioModel = _mapper.Map<Usuario>(usuarioLerDTO);
            if (_repository.Salvar(usuarioModel))
            {
                usuarioLerDTO = _mapper.Map<UsuarioLerDTO>(usuarioModel);

                //send async message
                try
                {
                    var usuarioPublishedDTO = _mapper.Map<UsuarioPublishedDTO>(usuarioLerDTO);
                    usuarioPublishedDTO.Evento = "Usuario_Atualizado";
                    _messageBusClient.PublishNewEvent(usuarioPublishedDTO);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
                }

                return CreatedAtRoute(nameof(GetUsuario), new { Id = usuarioLerDTO.Id }, usuarioLerDTO);
            }
            else
                return NotFound();
        }

        [Authorize]
        [HttpDelete]
        public IActionResult ApagarUsuario(int id)
        {
            Usuario usuario = _repository.SelecionarPeloId(id);
            UsuarioLerDTO usuarioLerDTO = _mapper.Map<UsuarioLerDTO>(usuario);

            _repository.Apagar(id);

            //send async message
            try
            {
                var usuarioPublishedDTO = _mapper.Map<UsuarioPublishedDTO>(usuarioLerDTO);
                usuarioPublishedDTO.Evento = "Usuario_Removido";
                _messageBusClient.PublishNewEvent(usuarioPublishedDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return Ok();

        }
    }
}
