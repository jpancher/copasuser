using NUnit.Framework;
using UsuariosService.Data;
using UsuariosService.Repository;
using Microsoft.EntityFrameworkCore;
using UsuariosService.Models;
using System.Linq;
using System;

namespace UsuariosService.test
{
    public class UsuarioRepoTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "testDB")
            .Options;

        AppDbContext context;
        UsuarioRepo repository;
        
        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            repository = new UsuarioRepo(context);
            Seed();
        }

        private void Seed()
        {
            repository.Criar(new Usuario { Nome = "TestUser1", Email = "testuser1@gmail.com" });
            repository.Criar(new Usuario { Nome = "TestUser2", Email = "testuser2@gmail.com" });
            repository.Criar(new Usuario { Nome = "TestUser3", Email = "testuser3@gmail.com" });
            repository.Criar(new Usuario { Nome = "TestUser4", Email = "testuser4@gmail.com" });
            repository.Criar(new Usuario { Nome = "TestUser5", Email = "testuser5@gmail.com" });
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }


        [Test]
        public void UsuarioRepo_Criar_UsuarioTestUser6Criado()
        {            
            Usuario usuario = new Usuario
            {
                Nome = "TestUser6",
                Email = "testuser6@gmail.com"
            };
            repository.Criar(usuario);
            var newUser = repository.SelecionarTodos().FirstOrDefault(el => el.Nome == usuario.Nome && el.Email == usuario.Email);
            Assert.IsInstanceOf<Usuario>(newUser);
            Assert.Pass();
        }

        [Test]
        public void UsuarioRepo_Criar_UsuarioTestUser1Alterado()
        {
            var usuarioOrigem = repository.SelecionarTodos().FirstOrDefault(el => el.Nome == "TestUser1" && el.Email == "testuser1@gmail.com");
            Usuario usuario = new Usuario
            { 
                Id = usuarioOrigem.Id,
                Nome = usuarioOrigem.Nome + "Alterado",
                Email = usuarioOrigem.Email
            };
            repository.Salvar(usuario);

            var usuarioAlterado = repository.SelecionarTodos().FirstOrDefault(el => el.Nome == usuario.Nome && el.Email == usuario.Email);
            Assert.IsInstanceOf<Usuario>(usuarioAlterado);
            Assert.Pass();
        }

        [Test]
        public void UsuarioRepo_Criar_UsuarioTestUser2Removido()
        {
            var usuario = repository.SelecionarTodos().FirstOrDefault(el => el.Nome == "TestUser2" && el.Email == "testuser2@gmail.com");
            repository.Apagar(usuario.Id);

            var usuarioRemovido = repository.SelecionarTodos().FirstOrDefault(el => el.Nome == usuario.Nome && el.Email == usuario.Email);
            Assert.IsNotInstanceOf<Usuario>(usuarioRemovido);
            Assert.Pass();
        }
    }
}