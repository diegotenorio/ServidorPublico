# 🏛️ Servidor Público API (.NET 8 + CQRS + MediatR + EF Core)

API RESTful para gerenciamento de servidores públicos com arquitetura limpa, CQRS, MediatR, FluentValidation, testes automatizados e Swagger.

## ✅ Funcionalidades

- `POST /api/servidores`: Cadastra um servidor
- `GET /api/servidores`: Lista servidores com filtros (nome, orgao, lotacao)
- `PUT /api/servidores/{id}`: Atualiza servidor
- `DELETE /api/servidores/{id}`: Inativa servidor (exclusão lógica)

## ⚙️ Tecnologias e Padrões

- .NET 8 + Web API
- Entity Framework Core (InMemory)
- CQRS com MediatR
- FluentValidation
- Testes com xUnit
- Swagger (UI em `/swagger`)
- Tratamento de erros global com middleware
- Dockerfile para containerização

## 🚀 Executando localmente

```bash
dotnet restore
dotnet build
dotnet run --project ServidorPublico.API
```

Acesse: [https://localhost:5001/swagger](https://localhost:5001/swagger)

## 🐳 Executando com Docker

```bash
docker build -t servidor-api .
docker run -p 5000:80 servidor-api
```

## 📁 Organização

- `Domain`: Entidades de domínio
- `Application`: Commands, Queries, Handlers, Validators
- `Infrastructure`: DbContext e Repositórios
- `API`: Controllers, Middlewares e Program
- `Tests`: Testes unitários dos Handlers

---
Feito com 💻 e foco em arquitetura limpa para sua prova técnica!
