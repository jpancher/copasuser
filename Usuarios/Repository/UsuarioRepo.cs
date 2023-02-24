using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UsuariosService.Data;
using UsuariosService.Models;

namespace UsuariosService.Repository
{
    public class UsuarioRepo : IUsuarioRepo
    {
        private readonly AppDbContext _context;

        public UsuarioRepo(AppDbContext context)
        {
            _context = context;
        }
        
        public void Apagar(int id)
        {
            _context.Usuarios.Remove(SelecionarPeloId(id));
            _context.SaveChanges();            
        }

        public void Criar(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public bool Salvar(Usuario usuario)
        {
            var src_usuario = SelecionarPeloId(usuario.Id);
            if (src_usuario == null)
                return false;
            
            src_usuario.Email = usuario.Email;
            src_usuario.Nome = usuario.Nome;            
            return _context.SaveChanges() > 0;
        }

        public Usuario SelecionarPeloId(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Usuario> SelecionarTodos()
        {
            Console.WriteLine(_context.Usuarios.ToList().Count());
            return _context.Usuarios.ToList();
        }
    }
}
