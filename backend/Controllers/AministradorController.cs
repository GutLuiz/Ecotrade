using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly AdministradorService _service = new();

        [HttpPut("aprovar/{creditoId}/{adminId}")]
        public IActionResult AprovarCredito(int creditoId, int adminId)
        {
            var resultado = _service.AprovarCredito(creditoId, adminId);

            if (resultado.Contains("sucesso"))
                return Ok(resultado);
            else
                return BadRequest(resultado);
        }
    }
}
