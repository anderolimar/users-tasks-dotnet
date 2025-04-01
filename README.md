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

### Autenticação
- **POST** `/api/auth/login` → Gere token de acesso.

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
   git clone https://github.com/anderolimar/users-tasks-dotnet
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

   # Using https
   dotnet run --launch-profile https
   ```
5. Acesse a documentação Swagger em:
   ```
   // http
   http://localhost:5036/scalar
 
   // https
   https://localhost:7274/scalar
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
- Adicionar testes de Controllers
- Resolver Warnings

## Licença
Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

