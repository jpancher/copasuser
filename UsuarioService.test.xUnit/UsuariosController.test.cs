using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;
using UsuariosService.Models;
using UsuariosService.Repository;
using UsuariosService.Dto;
using AutoMapper;
using UsuariosService.AsyncDataService;
using UsuariosService.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using FakeItEasy;
using System.Security.Cryptography;

namespace UsuarioService.test.xUnit
{
    public class UsuariosControllerTest
    {
        [Fact]
        public void UsuarioController_SelecionarTodos_ReturnOk()
        {
            //Arrange
            var repositorySub = A.Fake<IUsuarioRepo>();
            var mapperSub = A.Fake<IMapper>();
            var messageBusClientSub = A.Fake<IMessageBusClient>();
            var controller = new UsuariosService.Controllers.UsuariosController(
                repositorySub,
                mapperSub,
                messageBusClientSub);

            //Act
            var result = controller.GetUsuarios();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));            
        }

        [Fact]
        public void UsuarioController_SelecionarUm_ReturnUser()
        {
            //Arrange
            var repositorySub = A.Fake<IUsuarioRepo>();
            var mapperSub = A.Fake<IMapper>();
            var messageBusClientSub = A.Fake<IMessageBusClient>();
            var controller = new UsuariosService.Controllers.UsuariosController(
                repositorySub,
                mapperSub,
                messageBusClientSub);
            int anyNumber = 10;
            var usuario = A.Fake<Usuario>();
            A.CallTo(() => repositorySub.SelecionarPeloId(anyNumber)).Returns<Usuario>(usuario);

            //Act
            var result = controller.GetUsuario(anyNumber);

            //Assert
            result.Should().NotBeNull();  
            result.As<ActionResult<UsuarioLerDTO>>().Should().NotBeNull();
        }

        [Fact]
        public void UsuarioController_Criar_ReturnUser()
        {
            //Arrange
            var repositorySub = A.Fake<IUsuarioRepo>();
            var mapperSub = A.Fake<IMapper>();
            var messageBusClientSub = A.Fake<IMessageBusClient>();
            var controller = new UsuariosService.Controllers.UsuariosController(
                repositorySub,
                mapperSub,
                messageBusClientSub);
            var anyName = "aaa";
            var anyEmail = "aaa@";
            var newUser = new UsuarioCriarDTO { Nome = anyName, Email = anyEmail };

            //Act            
            var result = controller.CriarUsuario(newUser);

            //Assert
            result.Should().NotBeNull();
            result.As<CreatedAtRouteResult>().Value.Should().NotBeNull();            
        }

        [Fact]
        public void UsuarioController_Atualizar_ReturnUser()
        {
            //Arrange
            var repositorySub = A.Fake<IUsuarioRepo>();
            var mapperSub = A.Fake<IMapper>();
            var messageBusClientSub = A.Fake<IMessageBusClient>();
            var controller = new UsuariosService.Controllers.UsuariosController(
                repositorySub,
                mapperSub,
                messageBusClientSub);

            var anyId = 10;
            var anyName = "aaa";
            var anyEmail = "aaa@";
            var usuario = new Usuario { Id = anyId, Nome = anyName, Email = anyEmail };
            var usuarioLerDTO = mapperSub.Map<UsuarioLerDTO>(usuario);
            A.CallTo(() => repositorySub.Salvar(usuario)).Returns<bool>(true);
            A.CallTo(() => mapperSub.Map<Usuario>(usuarioLerDTO)).Returns(usuario);

            //Act            
            var result = controller.AtualizarUsuario(usuarioLerDTO);

            //Assert
            result.Should().NotBeNull();
            result.As<CreatedAtRouteResult>().Value.Should().NotBeNull();
        }

        [Fact]
        public void UsuarioController_Apagar_ReturnUser()
        {
            //Arrange
            var repositorySub = A.Fake<IUsuarioRepo>();
            var mapperSub = A.Fake<IMapper>();
            var messageBusClientSub = A.Fake<IMessageBusClient>();
            var controller = new UsuariosService.Controllers.UsuariosController(
                repositorySub,
                mapperSub,
                messageBusClientSub);
            int anyNumber = 10;
            var usuario = A.Fake<Usuario>();
            A.CallTo(() => repositorySub.SelecionarPeloId(anyNumber)).Returns<Usuario>(usuario);

            //Act
            var result = controller.ApagarUsuario(anyNumber);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
        }

    }
}
