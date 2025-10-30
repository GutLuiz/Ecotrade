using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("creditos")]
    public class CreditoController : ControllerBase
    {
        private readonly CreditoService _service = new CreditoService();

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] CreditoCarbono credito)
        {
            var resultado = _service.RegistrarCreditos(credito);
            return Ok(new { mensagem = resultado });
        }
    }
}
