using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly LoginService _loginService;

        public UsuariosController()
        {
            _loginService = new LoginService();
        }

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            var mensagem = _loginService.Registrar(usuario);
            return Ok(new { mensagem });
        }
    }
}
