using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ServidorPublico.Application.Commands;
using ServidorPublico.Application.Queries;

namespace ServidorPublico.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo gerenciamento dos servidores públicos.
    /// Expõe endpoints RESTful para criação, consulta, atualização e inativação de servidores.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ServidoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Construtor que injeta o serviço de MediatR para envio de comandos e queries.
        /// </summary>
        /// <param name="mediator">Instância de <see cref="IMediator"/>.</param>
        public ServidoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cadastra um novo servidor público no sistema.
        /// </summary>
        /// <param name="command">Comando contendo os dados do novo servidor.</param>
        /// <returns>Resposta 201 com o ID do servidor criado.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateServidorCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        /// <summary>
        /// Lista servidores públicos com filtros opcionais por nome, órgão e lotação.
        /// </summary>
        /// <param name="query">Query com os critérios de busca.</param>
        /// <returns>Lista de servidores encontrados.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetServidoresQuery query)
        {
            var servidores = await _mediator.Send(query);
            return Ok(servidores);
        }

        /// <summary>
        /// Atualiza os dados de um servidor público existente.
        /// </summary>
        /// <param name="id">Identificador do servidor (vindo da rota).</param>
        /// <param name="command">Comando contendo os dados atualizados.</param>
        /// <returns>
        /// Retorna 204 se atualizado com sucesso, 400 se o ID da rota diverge do corpo, ou 404 se não encontrado.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateServidorCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id do corpo não confere com o Id informado na URL.");

            var result = await _mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        /// <summary>
        /// Inativa (exclusão lógica) um servidor público pelo seu ID.
        /// </summary>
        /// <param name="id">Identificador do servidor a ser inativado.</param>
        /// <returns>204 se inativado com sucesso, ou 404 se não encontrado.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new InativarServidorCommand { Id = id });
            return result ? NoContent() : NotFound();
        }
    }
}
