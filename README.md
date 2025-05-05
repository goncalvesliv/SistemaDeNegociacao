# 🧾 Sistema de Negociação de Ativos
**Video Projeto Rodando** : https://drive.google.com/file/d/1K3fkBF4rF2gVY9Iq74HIibx0Nbuoiu1v/view?usp=sharing

## 📌 Descrição

Este projeto é um sistema de negociação de ativos com arquitetura distribuída. Ele simula a submissão e o processamento de ordens de compra e venda, realiza o casamento de ordens e salva os negócios realizados em um banco de dados SQL Server.

### A solução é composta por:

- **OrderAPI** – API para envio e consulta de ordens.  
- **OrderProcessor** – Serviço que consome mensagens do RabbitMQ, realiza o casamento de ordens e salva no banco.  
- **OrderUI** – Interface em Windows Forms para visualização de ordens e negócios.  
- **OrderCommonModels** – Biblioteca com modelos de dados compartilhados.  
- **OrdemApi.Tests** – Projeto de testes unitários com XUnit e Moq.

## ⚙️ Tecnologias utilizadas

- .NET 6 / .NET 8  
- C#  
- Windows Forms  
- RabbitMQ  
- Docker  
- SQL Server  
- XUnit + Moq  


## 🚀 Como executar o projeto

### 1. Clone o repositório

bash
git clone https://github.com/goncalvesliv/SistemaDeNegociacao.git
cd SistemaDeNegociacao

### 2. Configure a string de conexão com o banco
No projeto OrderAPI e OrderProcessor, edite o arquivo appsettings.json com a sua string de conexão:

"ConnectionStrings": {
  "NegociacoesDb": "Server=SEU_SERVIDOR;Database=NomeBanco;User Id=SEU_USUARIO;Password=SENHA;"
}

e depois crie duas tabelas no SqlServer 

CREATE TABLE Negocios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NomeAtivo NVARCHAR(50) NOT NULL,
    Preco DECIMAL(18,6) NOT NULL,
    Quantidade INT NOT NULL,
);

CREATE TABLE OrdemProcessada (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TipoOrdem NVARCHAR(1) NOT NULL,
    NomeAtivo NVARCHAR(50) NOT NULL,
    Preco DECIMAL(18,6) NOT NULL,
    Quantidade INT NOT NULL,
    Status NVARCHAR(30) NOT NULL,
);

### 3. Como executar o RabbitMQ com Docker
Para que a API publique e o processor consuma as ordens, é necessário que o RabbitMQ esteja rodando.

Passos:
Certifique-se de ter o Docker instalado.
Utilize o arquivo docker-compose.yml que está na raiz do projeto
Execute o comando : docker-compose up -d

Acesse a interface web
URL: http://localhost:15672
Usuário: guest
Senha: guest

### 4. Execute os projetos
OrderAPI - dotnet run
OrderProcessor (precisa do RabbitMQ rodando) - dotnet run
E rode a interface OrderUI 
