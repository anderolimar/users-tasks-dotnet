# Gerenciamento de Usuários e Tarefas - Web API

## Descrição
Web API em ASP.NET Core com Entity Framework para gerenciar usuários e tarefas.

## Tecnologias Utilizadas
- .NET 9
- ASP.NET Core
- Entity Framework Core (InMemory)
- JWT para autenticação
- Swagger para documentação (Scalar)


## Endpoints

### Usuários
- **POST** `/api/usuarios` → Criar um usuário.
- **GET** `/api/usuarios/{id}` → Buscar um usuário pelo ID.
- **GET** `/api/usuarios` → Listar todos os usuários.

### Tarefas
- **POST** `/api/usuarios/{id}/tarefas` → Criar uma tarefa para um usuário.
- **GET** `/api/usuarios/{id}/tarefas` → Listar todas as tarefas de um usuário.


## Configuração e Execução
1. Clone o repositório:
   ```sh
   git clone <URL_DO_REPOSITORIO>
   ```
2. Navegue até a pasta do projeto:
   ```sh
   cd UsersTasks
   ```
3. Instale as dependências:
   ```sh
   dotnet restore
   ```
4. Execute a API:
   ```sh
   dotnet run
   ```
5. Acesse a documentação Swagger em:
   ```
   http://localhost:<PORT>/scalar
   ```

## Usuario Test

- Usuario : user@test.com
- Senha : User@123

## Testes
Para executar os testes unitários:
```sh
dotnet test
```

## Debito Técnico
- Aadicionar testes de Controllers

## Licença
Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

