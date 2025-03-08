📌 API de Gestão de Projetos e Tarefas

Uma API RESTful desenvolvida em ASP.NET Core para gerenciar projetos e tarefas. Cada usuário pode criar projetos e adicionar tarefas, acompanhando o status de cada uma.

🚀 Tecnologias Utilizadas

ASP.NET Core 8

Entity Framework Core

Autenticação JWT

SQLite / SQL Server

FluentValidation

Swagger (Swashbuckle)

📜 Regras de Negócio

Cada usuário pode criar múltiplos projetos.

Um projeto pode conter várias tarefas.

Cada tarefa pode ter um dos seguintes status: Pendente, Em andamento, Concluída.

Um usuário não pode ter mais de 5 tarefas pendentes por projeto.

Apenas o criador do projeto pode adicionar, editar ou excluir suas tarefas.

🔗 Endpoints

📌 Autenticação (/auth)

POST /auth/register → Cadastro de novo usuário

POST /auth/login → Geração de token JWT

👤 Usuários (/users)

GET /users/{id} → Obter informações do usuário autenticado

📂 Projetos (/projects)

POST /projects → Criar um novo projeto

GET /projects → Listar projetos do usuário logado

GET /projects/{id} → Obter detalhes de um projeto

PUT /projects/{id} → Atualizar projeto

DELETE /projects/{id} → Remover projeto (e suas tarefas)

✅ Tarefas (/tasks)

POST /tasks → Criar uma nova tarefa dentro de um projeto

GET /tasks?projectId={id} → Listar tarefas de um projeto

GET /tasks/{id} → Obter detalhes de uma tarefa

PUT /tasks/{id} → Atualizar tarefa (exemplo: mudar status)

DELETE /tasks/{id} → Excluir tarefa

⚡ Instalação e Execução

1️⃣ Clone o Repositório

git clone https://github.com/seu-usuario/nome-do-repositorio.git
cd nome-do-repositorio

2️⃣ Configure o Banco de Dados

Altere a connection string no appsettings.json conforme o banco desejado (SQLite, SQL Server, etc.)

Execute as migrations:

dotnet ef database update

3️⃣ Execute a API

dotnet run

A API estará disponível em http://localhost:5000.

4️⃣ Acesse a Documentação (Swagger)

Abra o navegador e acesse:

http://localhost:5000/swagger

🎯 Funcionalidades Extras

✅ Paginação e Filtros no endpoint GET /tasks (exemplo: ?status=pendente).✅ Upload de Arquivos para anexar documentos aos projetos.✅ Envio de Notificações ao concluir uma tarefa.✅ Logs e Monitoramento com Serilog.



