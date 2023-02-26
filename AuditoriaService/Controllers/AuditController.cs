using AuditoriaService.Models;
using AuditoriaService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;

namespace AuditoriaService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IUsuarioAuditRepo _repository;

        public AuditController(IUsuarioAuditRepo repository)
        {
            _repository = repository;
        }

        [HttpPost]
        private ActionResult insereAudit(UsuarioAudit usuarioAudit)
        {
            _repository.insereUsuarioAudit(usuarioAudit);
            return Ok();
        }

    }
}
