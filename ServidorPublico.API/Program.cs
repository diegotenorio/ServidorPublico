/*using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using ServidorPublico.Application.Validators;
using ServidorPublico.Infrastructure;
using ServidorPublico.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CreateServidorValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependência
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("ServidorDb"));

builder.Services.AddMediatR(Assembly.Load("ServidorPublico.Application"));

builder.Services.AddValidatorsFromAssemblyContaining<CreateServidorValidator>();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

// Middleware global de erros
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();
app.MapControllers();
app.Run();*/

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

var builder = WebApplication.CreateBuilder(args);

// Serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ServidorDb"));
builder.Services.AddMediatR(Assembly.Load("ServidorPublico.Application"));
builder.Services.AddValidatorsFromAssemblyContaining<CreateServidorValidator>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



var app = builder.Build();

// Pipeline
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => Results.Ok("API de Servidores Públicos está no ar!"));

app.Run();
