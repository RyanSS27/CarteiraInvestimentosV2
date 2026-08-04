# 📈 CarteiraInvestimentos - Web API


<p align="center">
  <img src="https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white" alt=".NET 10">
  <img src="https://img.shields.io/badge/C%23-13-239120?logo=csharp&logoColor=white" alt="C# 13">
  <img src="https://img.shields.io/badge/MongoDB-8.2-47A248?logo=mongodb&logoColor=white" alt="MongoDB">
  <img src="https://img.shields.io/badge/API-REST-009688" alt="REST API">
  <img src="https://img.shields.io/badge/Architecture-Hexagonal-orange" alt="Hexagonal Architecture">
</p>

API REST desenvolvida em .NET 10 para gerenciamento de carteiras de investimentos em ativos negociados na B3, afim de consolidar boas práticas de arquitetura e integração com servições externos.

A aplicação permite cadastrar clientes, registrar operações de compra e venda, consultar o histórico de transações e consolidar automaticamente a carteira utilizando cotações obtidas pela Brapi. O projeto utiliza MongoDB e segue a Arquitetura Hexagonal (Ports and Adapters), mantendo o domínio desacoplado da infraestrutura.

---

## 🚀 Funcionalidades

- **Gerenciamento de clientes:** cadastro, consulta, atualização, ativação e inativação.
- **Registro de transações:** operações de Compra e Venda de ativos.
- **Histórico de operações:** consulta das transações realizadas por um cliente.
- **Consolidação da carteira:** cálculo de quantidade, preço médio, valor investido, valor atual e rentabilidade dos ativos.
- **Cotações atualizadas:** integração com a API Brapi para obtenção dos preços atuais dos ativos.

---

## 🛠️ Tecnologias

- **.NET 10 / C#**
- **MongoDB**
- **MongoDB.Driver**
- **Flurl**
- **Docker**
- **Scalar**
- **Postman**

### Arquitetura e conceitos

- Ports and Adapters (Arquitetura Hexagonal)
- Domain-Driven Design
- Princípios SOLID
- Injeção de Dependência
- Repository Pattern
- DTOs
- Programação Orientada a Objetos
- REST

---

## 🏗️ Estrutura do Projeto
A estrutura abaixo organiza a aplicação separando as responsabilidades afim de baixo acoplamento, isolando o núcleo de negócios (domínio) das ferramentas de tecnologia (infraestrutura).
```text
📁 CarteiraInvestimentosAPI/
├── 📁 Adapters/
│   ├── 📁 Controllers/
│   └── 📁 Infrastructure/
│       ├── 📁 ExternalServices/
│       ├── 📁 Repositories/
│       └── 📄 GlobalExceptionHandler.cs
│
├── 📁 Domain/
│   ├── 📁 Entities/
│   ├── 📁 Exceptions/
│   └── 📁 Services/
│       └── 📁 Ports/
│
├── 📁 Dtos/
│   ├── 📁 CustomersDtos/
│   └── 📁 WalletDtos/
│
└── 📄 Program.cs
```
## ⚙️ Configuração do Ambiente Local

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)
- Chave da API Brapi *(opcional para consultas públicas, utilizada neste projeto para rastreabilidade)*

### 1. Clonar o projeto

```bash
git clone https://github.com/RyanSS27/CarteiraInvestimentosAPI.git
cd CarteiraInvestimentosAPI
dotnet restore
```

### 2. Executar o MongoDB

O projeto utiliza MongoDB executado através do Docker:

```bash
docker run -d -p 27017:27017 --name mongodb-carteira mongo:latest
```

O banco é executado localmente na porta padrão `27017`.

### 3. Configurar User Secrets

As informações de conexão com o MongoDB e a chave da Brapi não são armazenadas no repositório.

Configure os valores utilizando o **User Secrets** do .NET:

```bash
dotnet user-secrets set "CarteiraInvestimentosAPI:ConnectionString" "mongodb://localhost:27017"
dotnet user-secrets set "Brapi:Token" "SUA_CHAVE_AQUI"
```

As demais configurações permanecem no `appsettings.json`:

```json
{
  "CarteiraInvestimentosAPI": {
    "DatabaseName": "CarteiraInvestimentos",
    "CustomersCollectionName": "Customers",
    "TransactionsCollectionName": "Transactions"
  },
  "Brapi": {
    "BaseUrl": "https://brapi.dev/api/v2/stocks/quote"
  }
}
```

### 4. Executar a aplicação

```bash
dotnet run
```

---

## 🧪 Como utilizar a API

A API possui documentação interativa através do **Scalar** e pode ser testada também utilizando o **Postman**.

### Principais endpoints

| Método | Rota | Descrição |
| :--- | :--- | :--- |
| **POST** | `/api/customer` | Cadastra um novo cliente |
| **GET** | `/api/customer/{id}` | Consulta um cliente |
| **POST** | `/api/wallet/{customerId}/transactions` | Registra uma transação |
| **GET** | `/api/wallet/{customerId}/transactions` | Consulta o histórico de transações |
| **GET** | `/api/wallet/{customerId}/summary` | Consulta a posição consolidada da carteira |

### 1. Cadastrar cliente

**POST**

```text
/api/customer
```

**Body:**

```json
{
  "name": "Ryan Souza",
  "email": "ryan@email.com"
}
```

### 2. Registrar uma transação

**POST**

```text
/api/wallet/{customerId}/transactions
```

**Body:**

```json
{
  "ticker": "PETR4",
  "quantity": 10,
  "unitPrice": 40.50,
  "transactionType": "BUY"
}
```

Os tipos de operação disponíveis são:

- `BUY` (ou 0) — compra
- `SELL` (ou 1) — venda

### 3. Consultar transações

**GET**

```text
/api/wallet/{customerId}/transactions?limit=10
```

Retorna o histórico de operações do cliente, limitado pela quantidade informada na consulta.

### 4. Consultar a carteira

**GET**

```text
/api/wallet/{customerId}/summary
```

A consulta consolida as informações dos ativos e, quando disponível, utiliza a cotação atual obtida através da Brapi.

Exemplo simplificado:

```json
{
  "ownerName": "Ryan Souza",
  "totalValue": 942.72,
  "totalValueUpToDate": 162.72,
  "totalValueEstimated": 780.00,
  "assets": [
    {
      "ticker": "PETR4",
      "currentQuantity": 4,
      "averagePrice": 30.00,
      "currentMarketPrice": 40.68,
      "totalInvestedValue": 120.00,
      "totalCurrentValue": 162.72,
      "returnPercentage": 35.60,
      "profitOrLoss": 42.72,
      "isPriceUpToDate": true
    }
  ]
}
```

A organização segue o princípio de Ports and Adapters, mantendo as regras de negócio no domínio e isolando detalhes de infraestrutura, como MongoDB e serviços externos.

---

## 📊 Integração com a Brapi

A aplicação consulta a **Brapi** para obter as cotações atuais dos ativos.

Embora a API forneça diversas informações sobre cada ativo, a aplicação utiliza apenas os dados necessários para a consolidação da carteira, como:

- Ticker;
- Preço atual.

A chave utilizada nas consultas é armazenada através do **User Secrets**, evitando sua exposição no código-fonte.

---

## 🗄️ Persistência

Os dados são persistidos em duas coleções utilizando o `MongoDB.Driver`:

- `Customers`
- `Transactions`

O banco é executado localmente através de um container Docker.

---

## 🏁 Conclusão

Este projeto demonstra a implementação de uma API REST para gerenciamento de carteiras de investimentos utilizando **.NET 10**, **MongoDB** e **Arquitetura Hexagonal**.

O código foi organizado priorizando separação de responsabilidades, baixo acoplamento e facilidade de manutenção, servindo como uma demonstração prática da aplicação desses conceitos em um projeto realista.
