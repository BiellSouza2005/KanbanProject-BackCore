# Kanban Project - Back-End (Core)

## 📖 Sobre o Projeto

Este repositório contém o **backend** do projeto Kanban, uma aplicação desenvolvida para otimizar o gerenciamento de tarefas em equipe. A plataforma permite a criação, atribuição e acompanhamento do progresso de atividades com diferentes níveis de permissão para administradores e usuários comuns.

A interface do usuário (frontend) consome a API desenvolvida neste projeto para exibir e manipular os dados.

### Funcionalidades do Kanban

O sistema foi desenhado com dois níveis de acesso:

* **👑 Admin:** Possui controle total sobre as tarefas (criar, editar, deletar, atribuir) e pode mover cards livremente entre as colunas do Kanban. É o único perfil que pode marcar uma tarefa como "Completed".
* **👤 Usuário Comum:** Pode visualizar todas as tarefas do time e mover os cards (seus e de outros) entre as colunas permitidas, seguindo o fluxo de trabalho. Todas as movimentações são registradas, garantindo o rastreamento das ações.

## 🎯 Finalidade deste Repositório

Este repositório é dedicado exclusivamente à **API RESTful** que serve como a espinha dorsal do projeto Kanban. Ele é responsável por:

* Implementar toda a lógica de negócios e as regras do sistema.
* Gerenciar a comunicação com o banco de dados (persistência de dados).
* Controlar a autenticação e as permissões de usuário.
* Expor os *endpoints* que são consumidos pela aplicação frontend (React).

## 💻 Tecnologias e Design Patterns

A construção desta API foi baseada em tecnologias e padrões de projeto robustos do ecossistema .NET:

* **[C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)**: Linguagem de programação principal.
* **[ASP.NET Core MVC](https://docs.microsoft.com/pt-br/aspnet/core/mvc/overview)**: Framework para a construção da aplicação web e da API.
* **[Entity Framework Core](https://docs.microsoft.com/pt-br/ef/core/)**: ORM (Object-Relational Mapper) para interagir com o banco de dados SQL Server.
* **[SQL Server](https://www.microsoft.com/pt-br/sql-server)**: Sistema de gerenciamento de banco de dados.
* **[Swagger (Swashbuckle)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)**: Ferramenta para documentação e teste interativo dos endpoints da API.

O projeto foi desenvolvido utilizando o **Visual Studio**.

## 🚀 Como Rodar o Projeto (Backend)

Siga os passos abaixo para configurar e executar o backend em seu ambiente local.

### Pré-requisitos

* [Visual Studio](https://visualstudio.microsoft.com/pt-br/) (com a carga de trabalho ASP.NET e desenvolvimento web)
* [.NET SDK](https://dotnet.microsoft.com/download) (versão compatível com o projeto)
* [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

### Passo a Passo

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/BiellSouza2005/KanbanProject-BackCore.git
    ```
    * **⚠️ Observação Importante:** Certifique-se de que o repositório esteja na branch **master**, caso não esteja:
      
    ```bash
    git checkout master
    ```
      
2.  **Acesse a pasta do projeto:**
    ```bash
    cd KanbanProject-BackCore
    ```

3.  **Abra a Solution (`.sln`) no Visual Studio.**

4.  **Configure a Connection String:**
    * No *Solution Explorer*, localize e abra o arquivo `appsettings.json`.
    * Substitua o valor da `DefaultConnection` pela connection string do seu banco de dados SQL Server.

    *String de conexão padrão (exemplo a ser substituído):*
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "server=localhost,1433;database=KabanProjectDB;User Id=sa;Password=Batata@123#;TrustServerCertificate=True"
    }
    ```

5.  **Aplique as Migrations:**
    * Abra o *Package Manager Console* no Visual Studio (`View > Other Windows > Package Manager Console`) ou um terminal na raiz do projeto.
    * Execute o seguinte comando para criar o banco de dados e aplicar as tabelas:
    ```bash
    dotnet ef database update --startup-project .\KanbanProject --project .\KanbanProject
    ```

6.  **Execute a Aplicação:**
    * Pressione `F5` ou clique no botão de play do Visual Studio para iniciar a aplicação.
    * **⚠️ Observação Importante:** Certifique-se de que o perfil de execução está configurado para **HTTP**, e não HTTPS. O projeto deve ser rodado em HTTP.

7.  Pronto! A API estará em execução e a interface do Swagger será aberta automaticamente no seu navegador, permitindo que você visualize e teste todos os endpoints disponíveis.

## 📝 Observações Importantes

### Como Tornar um Usuário Admin

Por padrão, todo novo usuário criado através do endpoint de registro inicia com permissões de usuário comum (sem Admin). Para conceder privilégios de administrador:

1.  Crie um usuário normalmente.
2.  Copie o `Id` do usuário recém-criado.
3.  Na interface do Swagger, encontre o endpoint: `PUT /api/User/UpdateUserPermission/{id}`.
4.  Clique em "Try it out", cole o `Id` do usuário no campo de parâmetro e execute a requisição.

Isso alterará a permissão do usuário para Admin.

## 🤝 Colaboradores

Agradecemos às seguintes pessoas que contribuíram para este projeto:

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/biellSouza2005" title="GitHub do Gabriel Souza">
        <img src="https://github.com/biellSouza2005.png" width="100px;" alt="Foto do Gabriel Souza no GitHub"/><br>
        <sub>
          <b>Gabriel Souza</b>
        </sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/GustavoAlvesValadao" title="GitHub do Gustavo Valadao">
        <img src="https://github.com/GustavoAlvesValadao.png" width="100px;" alt="Foto do Gustavo Valadao no GitHub"/><br>
        <sub>
          <b>Gustavo Valadao</b>
        </sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/ArthurBerthi505" title="GitHub do Arthur Berthi">
        <img src="https://github.com/ArthurBerthi505.png" width="100px;" alt="Foto do Arthur Berthi no GitHub"/><br>
        <sub>
          <b>Arthur Berthi</b>
        </sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/Freitasss2005" title="GitHub do Maria Freitas">
        <img src="https://github.com/Freitasss2005.png" width="100px;" alt="Foto do Maria Freitas no GitHub"/><br>
        <sub>
          <b>Maria Freitas</b>
        </sub>
      </a>
    </td>
  </tr>
</table>
