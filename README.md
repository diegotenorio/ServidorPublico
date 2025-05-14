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


## 🚀 Executando GitHub Codespaces

```bash
dotnet restore
dotnet build
dotnet run --project ServidorPublico.API
```

Acesse: [https://jubilant-waddle-xvg5vxqq64hp7pj-5000.app.github.dev/swagger/](https://jubilant-waddle-xvg5vxqq64hp7pj-5000.app.github.dev/swagger/)

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


## 📦 Exemplos de Requisições

### ➕ POST `/api/servidores`

```json
{
  "nome": "João da Silva",
  "telefone": "(61) 99999-8888",
  "email": "joao@orgao.gov.br",
  "orgaoId": 1,
  "lotacaoId": 2,
  "sala": "A-102"
}
```

### 🔄 PUT `/api/servidores/1`

```json
{
  "nome": "João da Silva Santos",
  "telefone": "(61) 98888-7777",
  "email": "joao.santos@orgao.gov.br",
  "orgaoId": 1,
  "lotacaoId": 3,
  "sala": "A-202"
}
```

### 🔍 GET `/api/servidores?nome=joao&orgaoId=1&lotacaoId=2`

Retorna uma lista de servidores com os filtros aplicados por nome, órgão e lotação.

### ❌ DELETE `/api/servidores/1`

Inativa logicamente o servidor com o ID 1.

---