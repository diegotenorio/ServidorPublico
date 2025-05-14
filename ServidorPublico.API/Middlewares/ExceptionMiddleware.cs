using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ServidorPublico.API.Middlewares
{
    /// <summary>
    /// Middleware de tratamento global de exceções.
    /// Intercepta exceções durante o pipeline HTTP e retorna uma resposta JSON padronizada.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Construtor que recebe os serviços injetados necessários.
        /// </summary>
        /// <param name="next">Delegado que representa o próximo middleware no pipeline.</param>
        /// <param name="logger">Logger utilizado para registrar as exceções capturadas.</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Executa o middleware, capturando e tratando exceções lançadas durante a requisição.
        /// </summary>
        /// <param name="context">Contexto da requisição HTTP.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("Erro de validação capturado: {mensagens}", ex.Errors);

                await EscreverRespostaJsonAsync(context, HttpStatusCode.BadRequest, new
                {
                    status = 400,
                    errors = ex.Errors.Select(e => e.ErrorMessage)
                });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Recurso não encontrado: {mensagem}", ex.Message);

                await EscreverRespostaJsonAsync(context, HttpStatusCode.NotFound, new
                {
                    status = 404,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno inesperado.");

                await EscreverRespostaJsonAsync(context, HttpStatusCode.InternalServerError, new
                {
                    status = 500,
                    message = "Ocorreu um erro inesperado no servidor."
                });
            }
        }

        /// <summary>
        /// Serializa o objeto de erro em JSON e escreve no corpo da resposta HTTP.
        /// </summary>
        /// <param name="context">Contexto da requisição HTTP.</param>
        /// <param name="statusCode">Código de status HTTP a ser retornado.</param>
        /// <param name="responseObject">Objeto anônimo contendo a mensagem ou erros.</param>
        private static async Task EscreverRespostaJsonAsync(HttpContext context, HttpStatusCode statusCode, object responseObject)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(responseObject);
            await context.Response.WriteAsync(json);
        }
    }
}
