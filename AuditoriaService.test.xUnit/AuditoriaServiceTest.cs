using System;
using Xunit;
using AuditoriaService;
using FakeItEasy;
using AuditoriaService.Repository;
using AuditoriaService.Data;
using AuditoriaService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuditoriaService.test.xUnit
{
    public class AuditoriaServiceTest
    {
        [Fact]
        public void AuditoriaRepository_Insere_AsyncTaskIsCompleted()
        {
            //Arrange
            var contextSub = A.Fake<IUsuarioAuditContext>();                
            var repositorySub = new UsuarioAuditRepo(contextSub);
            var anyId = 10;
            var anyName = "aaa";
            var anyEmail = "aaa@";
            var anyEvento = "Usuario_Inserido";
            var usuarioAudit = new UsuarioAudit
            {
                DataHora = new DateTime(),
                Id = anyId,
                Nome = anyName,
                Email = anyEmail,
                Evento = anyEvento
            };                

            //Act            
            var task = repositorySub.insereUsuarioAudit(usuarioAudit);

            //Assert
            task.GetAwaiter().GetResult();
            Assert.True(task.IsCompleted);
        }

    }
}
