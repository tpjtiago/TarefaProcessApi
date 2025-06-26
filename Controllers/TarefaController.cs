using Microsoft.AspNetCore.Mvc;
using TarefaProcessorApi.Services;

namespace TarefaProcessorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaProcessor _processor;

        public TarefaController(TarefaProcessor processor)
        {
            _processor = processor;
        }

        [HttpPost("processar-tarefas")]
        public async Task<IActionResult> Processar()
        {
            await _processor.ProcessarTarefasAsync();
            return Ok("Processamento iniciado.");
        }
    }
}
