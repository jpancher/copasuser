using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UsuariosService.Dto
{
    public class UsuarioCriarDTO
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
