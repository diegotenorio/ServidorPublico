using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using FluentValidation;
using ServidorPublico.Infrastructure;
using ServidorPublico.API.Middlewares;
using ServidorPublico.Application.Behaviors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// =====================================
// REGISTRO DE SERVIÇOS DA APLICAÇÃO
// =====================================

// Controladores da API
builder.Services.AddControllers();

// Suporte ao Swagger (documentação da API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Banco de dados em memória para testes locais e desenvolvimento
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("ServidorDb"));

// Registro do MediatR com base na assembly de comandos e queries
builder.Services.AddMediatR(Assembly.Load("ServidorPublico.Application"));

// Registro automático de validadores FluentValidation da mesma assembly dos comandos
builder.Services.AddValidatorsFromAssemblyContaining<CreateServidorValidator>();

// Adiciona o behavior global que executa validações antes dos handlers MediatR
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Versionamento da API
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);                     // Define versão padrão: v1.0
    opt.AssumeDefaultVersionWhenUnspecified = true;                   // Assume a versão padrão se não for informada
    opt.ReportApiVersions = true;                                     // Exibe as versões suportadas nos headers da resposta
});

var app = builder.Build();

// =====================================
// CONFIGURAÇÃO DO PIPELINE HTTP
// =====================================

// Middleware de tratamento global de exceções (converte exceções em respostas padronizadas)
app.UseMiddleware<ExceptionMiddleware>();

// Middleware do Swagger: gera e exibe documentação interativa da API
app.UseSwagger();
app.UseSwaggerUI();

// Middleware de autorização (a autenticação não está configurada aqui, mas pode ser adicionada)
app.UseAuthorization();

// Mapeia os controllers e endpoints da API
app.MapControllers();

// Endpoint raiz para verificação de status da API
app.MapGet("/", () => Results.Ok("API de Servidores Públicos está no ar!"));

// Inicia a aplicação ASP.NET Core
app.Run();
