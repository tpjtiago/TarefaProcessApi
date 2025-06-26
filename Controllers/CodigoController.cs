using Microsoft.AspNetCore.Mvc;
using TarefaProcessorApi.Models;
using TarefaProcessorApi.Services;

namespace TarefaProcessorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodigoController : ControllerBase
    {
        private readonly CodigoProcessor _processor;

        public CodigoController(CodigoProcessor processor)
        {
            _processor = processor;
        }

        [HttpPost("processar")]
        public async Task<IActionResult> ProcessarCodigo([FromBody] CodigoRequest request)
        {
            var sucesso = await _processor.ProcessarCodigoAsync(request.Codigo);

            if (sucesso)
                return Ok("Código processado com sucesso.");
            else
                return Conflict("Código já está em processamento ou foi processado recentemente.");
        }
    }
}
