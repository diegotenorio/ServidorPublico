using Microsoft.EntityFrameworkCore;
using ServidorPublico.Infrastructure;
using MediatR;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using ServidorPublico.Application.Validators;
using ServidorPublico.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateServidorValidator>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependência
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("ServidorDb"));

builder.Services.AddMediatR(Assembly.Load("ServidorPublico.Application"));

builder.Services.AddValidatorsFromAssemblyContaining<CreateServidorValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware global de erros
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();
app.MapControllers();
app.Run();
