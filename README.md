# Exemplo de Cache em Memoria

## Sobre o projeto

Este projeto serve como modelo para implementar memoria cache basica de maneira generica. Ele foca em ser direto, objeto e simples ao mesmo tempo que é muito eficiente 

Importante, assim que rodar o projeto ele vai popular o banco de dados com 1.000.000 registros aleatorios usando a lib Bogus, caso não queira apenas comente a linha 18 na Produto/Produto.Infrastructure.Data/Contexts/ApplicationDbContextInitializer.cs


se não sabe o que esta fazendo tente entender o codigo antes de alterar (afim de te ajudar no seu proprio processo de apredizagem)

## Principais características

### **1. Principais recursos (concluído)**

- Estrutura de Arquitetura Limpa (Domínio, Aplicação, API Web, Infraestrutura)
- ASP.NET Core 8.0 com Entity Framework Core
- Suporte ao Docker com integração ao Postgresql
- Token JWT e autenticação por identidade
- Health Check
- Middleware para tratamento e validação de exceções
- Capturações de logs
- Https com certificados autoassinados

## Começando

### Pré-requisitos

- .NET 8.0 SDK
- Docker
- Postgresql

### Instalação

1. Clone o repositório:

   ```bash
   git clone https://github.com/FlavioMartinsJr/example-cache-inmemory
   ```

2. Execute:

   - Docker:

     ```bash
     docker-compose up --build
     ```

   - Local:

     ```bash
     dotnet run ./Produtos/Produtos.API.csproj
     ```
### Uso

API via:

- Docker: `https://localhost:3001/swagger/index.html`
 
- Local:
  - https:`https://localhost:7208/swagger/index.html`
  - http:`http://localhost:5118/swagger/index.html`
