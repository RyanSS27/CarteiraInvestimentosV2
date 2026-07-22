# 📈 CarteiraInvestimentosV2

API REST desenvolvida em **.NET 10** utilizando **MongoDB**, voltada ao gerenciamento de uma carteira de investimentos em renda variável.

Esta é a segunda versão do projeto, desenvolvida com foco na evolução da arquitetura e do domínio da aplicação. O desenvolvimento acontece de forma incremental, iniciando pela implementação das funcionalidades essenciais antes da integração com serviços externos.

---

## 🚀 Funcionalidades

- Cadastro e gerenciamento de clientes
- Registro de ativos e transações
- Consolidação da carteira de investimentos
- Cálculo de posição e preço médio
- Integração com a API Brapi *(em desenvolvimento)*

---

## 🛠️ Tecnologias

- .NET 10 (C#)
- MongoDB Community
- MongoDB.Driver
- REST API

---

## 📁 Estrutura do Projeto

```text
📁 CarteiraInvestimentosV2/
├── 📁 Controllers
├── 📁 Database
├── 📁 Dtos
├── 📁 Entities
├── 📁 Services
├── 📄 Program.cs
└── 📄 appsettings.json
```

A aplicação segue os princípios de **Ports and Adapters (Arquitetura Hexagonal)**, mantendo o domínio desacoplado das tecnologias de infraestrutura.

---

## ✅ Progresso

- [x] Estrutura inicial do projeto
- [x] Configuração do MongoDB
- [x] Modelagem das entidades
- [ ] CRUD de clientes
- [ ] Gerenciamento de ativos
- [ ] Registro de transações
- [ ] Consolidação da carteira
- [ ] Integração com a Brapi
- [ ] FluentValidation
- [ ] Flurl

---

## ⚙️ Executando o projeto

### Pré-requisitos

- .NET SDK 10
- MongoDB Community

### Clonar o repositório

```bash
git clone https://github.com/RyanSS27/CarteiraInvestimentosV2.git
cd CarteiraInvestimentosV2
dotnet restore
```

### Configuração

Configure a conexão com o MongoDB em `appsettings.json`.

```json
{
  "CarteiraInvestimentosAPI": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "CarteiraInvestimentos",
    "CustomersCollectionName": "Customers",
    "TransactionsCollectionName": "Transactions"
  }
}
```

### Executar

```bash
dotnet run
```

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/C%23-13-239120?logo=csharp&logoColor=white" alt="C# 13">
  <img src="https://img.shields.io/badge/MongoDB-Community-47A248?logo=mongodb&logoColor=white" alt="MongoDB">
  <img src="https://img.shields.io/badge/API-REST-009688" alt="REST API">
  <img src="https://img.shields.io/badge/Architecture-Hexagonal-orange" alt="Hexagonal Architecture">
</p>
