using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ServidorPublico.Application.Commands;
using ServidorPublico.Application.Queries;

namespace ServidorPublico.API.Controllers
{
    // Define que essa classe é um controller de API REST
    [ApiController]
    // Define o endpoint base como "api/servidores"
    [Route("api/[controller]")]
    public class ServidoresController : ControllerBase
    {
        // Injeção de dependência do MediatR para disparar comandos e queries
        private readonly IMediator _mediator;

        public ServidoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Endpoint para criar um novo servidor público
        // Exemplo de chamada: POST /api/servidores
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateServidorCommand command)
        {
            // Envia o comando para o handler via MediatR
            var id = await _mediator.Send(command);

            // Retorna 201 Created com o local do recurso recém-criado
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        // Endpoint para listar servidores com filtros (nome, orgao, lotacao)
        // Exemplo de chamada: GET /api/servidores?nome=João
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetServidoresQuery query)
        {
            // Envia a query para o handler e obtém os resultados
            var servidores = await _mediator.Send(query);
            return Ok(servidores);
        }

        // Endpoint para atualizar um servidor existente
        // Exemplo de chamada: PUT /api/servidores/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateServidorCommand command)
        {
            // Validação: impede inconsistência entre o id da URL e o do corpo
            if (id != command.Id) return BadRequest("Id do corpo não confere com o Id informado no filtro.");

            // Envia comando de atualização
            var result = await _mediator.Send(command);

            // Se não encontrou o servidor, retorna 404
            return result ? NoContent() : NotFound();
        }

        // Endpoint para inativar (exclusão lógica) um servidor
        // Exemplo de chamada: DELETE /api/servidores/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Cria e envia comando de inativação
            var result = await _mediator.Send(new InativarServidorCommand { Id = id });

            // Se não encontrou o servidor, retorna 404
            return result ? NoContent() : NotFound();
        }
    }
}
