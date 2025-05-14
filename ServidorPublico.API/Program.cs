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

// Serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ServidorDb"));
builder.Services.AddMediatR(Assembly.Load("ServidorPublico.Application"));
builder.Services.AddValidatorsFromAssemblyContaining<CreateServidorValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

//Versionamento
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
});

var app = builder.Build();

// Pipeline
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Ok("API de Servidores Públicos está no ar!"));

app.Run();
