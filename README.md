# ProcBlazor

ProcBlazor é um projeto que contém uma API e um front-end desenvolvido com Blazor, ambos em .NET 8.0 e localizados na mesma solução.

## Estrutura do Projeto

- **Presentation**: Contém o front-end em Blazor.
- **Api**: Contém a API da aplicação.

## Pré-requisitos

Antes de iniciar o projeto, certifique-se de ter as seguintes ferramentas instaladas em sua máquina:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [MySQL](https://dev.mysql.com/downloads/installer/)

## Banco de Dados

Você precisará de um banco de dados MySQL rodando com as seguintes tabelas criadas:

```sql
CREATE TABLE usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Senha VARCHAR(256) NOT NULL,
    CONSTRAINT Email UNIQUE (Email)
);

CREATE TABLE mensagens (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    IdUsuario INT NOT NULL,
    DataHora DATETIME NOT NULL,
    Mensagem TEXT NOT NULL,
    CONSTRAINT mensagens_ibfk_1 FOREIGN KEY (IdUsuario) REFERENCES usuarios(Id)
);

CREATE INDEX IdUsuario ON mensagens (IdUsuario);
```
## Clonando o Repositório

Para clonar o repositório, execute o seguinte comando:

```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
```

## Configuração da API

1. Navegue até o diretório da API:

    ```bash
    cd ProcBlazor/Api
    ```

2. Configure a string de conexão com o MySQL no arquivo `appsettings.json` ou `appsettings.Development.json`:

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=nome_do_banco;User=root;Password=sua_senha;"
    }
    ```

3. Inicie a API:

    ```bash
    dotnet run
    ```

A API estará rodando na porta **5000** por padrão. Acesse-a em: `http://localhost:5000`.

É possível acessar a documentação da api em `http://localhost:5000/swagger`


## Configuração do Front-end Blazor

1. Navegue até o diretório do front-end:

    ```bash
    cd ProcBlazor/Presentation
    ```

2. Inicie o projeto Blazor:

    ```bash
    dotnet run
    ```

O front-end será iniciado em uma porta aleatória. O terminal indicará qual porta está sendo utilizada.
