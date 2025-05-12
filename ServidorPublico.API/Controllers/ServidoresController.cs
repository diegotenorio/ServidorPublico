using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ServidorPublico.Application.Commands;
using ServidorPublico.Application.Queries;

namespace ServidorPublico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServidoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServidoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateServidorCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetServidoresQuery query)
        {
            var servidores = await _mediator.Send(query);
            return Ok(servidores);
        }
    }
}
