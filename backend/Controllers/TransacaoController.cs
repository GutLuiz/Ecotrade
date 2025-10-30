using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("transacoes")]
    public class TransacaoController : ControllerBase
    {
        private readonly TransacaoService _service = new TransacaoService();

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] Transacao transacao)
        {
            var resultado = _service.RegistrarTransacao(transacao);
            return Ok(new { mensagem = resultado });
        }
    }
}
