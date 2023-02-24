using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosService.Models;

namespace UsuariosService.Repository
{
    public interface IUsuarioRepo
    {
        void Criar(Usuario usuario);
        bool Salvar(Usuario usuario);
        void Apagar(int id);
        IEnumerable<Usuario> SelecionarTodos();
        Usuario SelecionarPeloId(int id);        
    }
}
