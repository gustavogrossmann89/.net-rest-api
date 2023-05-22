# Proposta

Realizar o desafio proposto na Trilha .NET - API e Entity Framework da www.dio.me

Depois disso, incrementar o projeto com outras classes, validações, testes unitários e etc, botando em prática demais conhecimentos adquiridos sobre a linguagem

## Desafio de projeto

Usar conhecimentos adquiridos no módulo de API e Entity Framework, da trilha .NET da DIO.

## Contexto

Classe tarefa (v1):

![Diagrama da classe Tarefa](diagrama.png)

**Swagger V1**

![Métodos Swagger](swagger.png)

**Endpoints**

| Verbo  | Endpoint               | Parâmetro | Body          |
| ------ | ---------------------- | --------- | ------------- |
| GET    | /Tarefa/{id}           | id        | N/A           |
| PUT    | /Tarefa/{id}           | id        | Schema Tarefa |
| DELETE | /Tarefa/{id}           | id        | N/A           |
| GET    | /Tarefa/ObterTodos     | N/A       | N/A           |
| GET    | /Tarefa/ObterPorTitulo | titulo    | N/A           |
| GET    | /Tarefa/ObterPorData   | data      | N/A           |
| GET    | /Tarefa/ObterPorStatus | status    | N/A           |
| POST   | /Tarefa                | N/A       | Schema Tarefa |

**Exemplo de Body para o POST:**

```json
{
  "id": 0,
  "titulo": "string",
  "descricao": "string",
  "data": "2022-06-08T01:31:07.056Z",
  "status": "Pendente"
}
```
