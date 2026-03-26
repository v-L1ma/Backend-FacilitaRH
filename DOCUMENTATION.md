# Documentação Completa — Backend FacilitaRH

> Gerada em: 2026-03-26  
> Objetivo: Referência completa para refatoração/migração para outra linguagem.

---

## Sumário

1. [Visão Geral](#visão-geral)
2. [Stack Tecnológica](#stack-tecnológica)
3. [Configuração e Variáveis de Ambiente](#configuração-e-variáveis-de-ambiente)
4. [Entidades (Modelos de Banco de Dados)](#entidades-modelos-de-banco-de-dados)
5. [Rotas da API](#rotas-da-api)
6. [Controllers e Regras de Negócio](#controllers-e-regras-de-negócio)
   - [Users (Usuários)](#users-usuários)
   - [Vacancies (Vagas)](#vacancies-vagas)
   - [Applications (Candidaturas)](#applications-candidaturas)
   - [Statistics (Estatísticas)](#statistics-estatísticas)
7. [Middleware de Autenticação](#middleware-de-autenticação)
8. [Fluxos de Autenticação](#fluxos-de-autenticação)
9. [Códigos de Resposta HTTP](#códigos-de-resposta-http)
10. [Observações para Migração](#observações-para-migração)

---

## Visão Geral

O **Backend FacilitaRH** é uma API REST para um sistema de Recursos Humanos que gerencia:

- **Usuários internos** (recrutadores/gestores de RH) com autenticação JWT
- **Vagas de emprego** (criação, edição, exclusão, listagem)
- **Candidaturas** de candidatos às vagas
- **Estatísticas** agregadas sobre vagas e candidatos

O frontend está hospedado em `https://facilita-rh.netlify.app` e é o único origin permitido via CORS.

---

## Stack Tecnológica

| Camada | Tecnologia | Versão |
|--------|-----------|--------|
| Linguagem | TypeScript | ^5.7.3 |
| Runtime | Node.js | — |
| Framework Web | Express | ^4.21.2 |
| ORM | Prisma | ^6.4.0 |
| Banco de Dados | SQLite (dev) | — |
| Autenticação | JSON Web Token (jsonwebtoken) | ^9.0.2 |
| Hash de Senha | bcryptjs | ^3.0.2 |
| CORS | cors | ^2.8.5 |
| Variáveis de Ambiente | dotenv | ^16.4.7 |
| Dev Server | ts-node-dev | ^2.0.0 |

**Porta:** `3000`  
**Comando de start:** `ts-node-dev --respawn --transpile-only --files src/index.ts`

---

## Configuração e Variáveis de Ambiente

| Variável | Tipo | Obrigatório | Descrição |
|----------|------|-------------|-----------|
| `SECRET` | `string` | **Sim** | Chave secreta usada para assinar e verificar tokens JWT. Se não definida, a aplicação lança erro em qualquer operação de auth. |

---

## Entidades (Modelos de Banco de Dados)

### `User` — Usuário do sistema (interno)

| Campo | Tipo | Restrições | Descrição |
|-------|------|-----------|-----------|
| `id` | `Int` | PK, auto-increment | Identificador único |
| `name` | `String` | NOT NULL | Nome completo |
| `email` | `String` | NOT NULL, UNIQUE | E-mail (usado no login) |
| `password` | `String` | NOT NULL | Senha armazenada como hash bcrypt (salt 10) |

---

### `Vacancy` — Vaga de emprego

| Campo | Tipo | Restrições | Descrição |
|-------|------|-----------|-----------|
| `id` | `Int` | PK, auto-increment | Identificador único |
| `status` | `String` | NOT NULL | Status da vaga (ex.: "Aberta", "Fechada") |
| `titulo` | `String` | NOT NULL | Título/nome do cargo |
| `qtdeVagas` | `Int` | NOT NULL | Quantidade de vagas disponíveis |
| `descricao` | `String` | NOT NULL | Descrição detalhada da vaga |
| `setor` | `String` | NOT NULL | Setor/departamento da vaga |
| `senioridade` | `String` | NOT NULL | Nível de senioridade exigido |
| `diversidade` | `String` | NOT NULL | Indicador de vaga de diversidade |
| `pcd` | `String` | NOT NULL | Indicador de vaga PCD (Pessoa com Deficiência) |
| `salario` | `String` | NOT NULL | Faixa salarial (armazenada como texto) |
| `contrato` | `String` | NOT NULL | Tipo de contrato (CLT, PJ, etc.) |
| `turno` | `String` | NOT NULL | Turno de trabalho |
| `local` | `String` | NOT NULL | Modelo de trabalho (Presencial, Remoto, Híbrido) |
| `endereco` | `String` | NOT NULL | Endereço físico |
| `dataAbertura` | `String` | NOT NULL | Data de abertura da vaga (formato string/ISO) |
| `dataFechamento` | `String` | NOT NULL | Data de fechamento da vaga (formato string/ISO) |

**Setores válidos conhecidos pelo sistema de estatísticas:**
- Administrativo, Financeiro, Comercial, Vendas, Marketing, Tecnologia da Informação, Atendimento ao Cliente, Logística, Jurídico, Produção / Manufatura, Compras / Suprimentos, Almoxarifado, Qualidade, Segurança do Trabalho

---

### `Application` — Candidatura de um candidato

| Campo | Tipo | Restrições | Descrição |
|-------|------|-----------|-----------|
| `id` | `Int` | PK, auto-increment | Identificador único |
| `vacancyID` | `Int` | NOT NULL, FK (lógica) | ID da vaga à qual o candidato se aplicou |
| `nomeCompleto` | `String` | NOT NULL | Nome completo do candidato |
| `email` | `String` | NOT NULL, UNIQUE | E-mail do candidato |
| `telefone` | `String` | NOT NULL | Telefone de contato |
| `dataNasc` | `String` | NOT NULL | Data de nascimento |
| `cpf` | `String` | NOT NULL, UNIQUE | CPF do candidato |
| `resumoProfissional` | `String` | NOT NULL | Resumo/objetivo profissional |
| `cargo` | `String` | NOT NULL | Último cargo ocupado |
| `empresa` | `String` | NOT NULL | Última empresa onde trabalhou |
| `dataInicioEmpresa` | `String` | NOT NULL | Data de início na última empresa |
| `dataTerminoEmpresa` | `String` | NOT NULL | Data de término na última empresa |
| `descricaoATVD` | `String` | NOT NULL | Descrição das atividades exercidas |
| `situacao` | `String` | NOT NULL | Situação atual (Empregado, Desempregado, etc.) |
| `escolaridade` | `String` | NOT NULL | Nível de escolaridade |
| `curso` | `String` | NOT NULL | Curso de formação |
| `instituicao` | `String` | NOT NULL | Instituição de ensino |
| `dataInicioEstudo` | `String` | NOT NULL | Data de início do curso |
| `dataTerminoEstudos` | `String` | NOT NULL | Data de término do curso |

> **Nota:** O campo `vacancyID` referencia `Vacancy.id`, mas **não há ForeignKey explícita** no schema Prisma — é uma relação lógica gerenciada pela aplicação.

---

## Rotas da API

**Base URL:** `http://localhost:3000`

### Tabela Resumo

| Método | Rota | Auth | Controller | Descrição |
|--------|------|------|-----------|-----------|
| `POST` | `/users` | ❌ | CreateUserController | Criar novo usuário |
| `GET` | `/users` | ✅ JWT | GetUserController | Listar todos os usuários |
| `POST` | `/users/auth` | ❌ | AuthUserController | Autenticar usuário (login) |
| `GET` | `/applications/` | ❌ | GetAllApplicationsController | Listar todas as candidaturas |
| `POST` | `/applications/:vacancyID` | ❌ | CreateApplicationController | Candidatar-se a uma vaga |
| `GET` | `/applications/:vacancyID` | ❌ | GetApplicationsController | Listar candidaturas de uma vaga |
| `POST` | `/vacancies` | ❌ | CreateVacancyController | Criar nova vaga |
| `GET` | `/vacancies` | ❌ | GetVacancyController | Listar todas as vagas |
| `GET` | `/vacancies/:vacancyID` | ❌ | GetVacancyInfoController | Detalhes de uma vaga |
| `DELETE` | `/vacancies/:vacancyID` | ❌ | DeleteVacancyController | Deletar uma vaga |
| `PUT` | `/vacancies/:vacancyID` | ❌ | UpdateVacancyController | Atualizar uma vaga |
| `GET` | `/statistics` | ❌ | GetStatisticsController | Retornar estatísticas gerais |

> ⚠️ **Atenção para migração:** Apenas a rota `GET /users` possui proteção via `AuthMiddleware`. Todas as demais rotas são públicas.

---

## Controllers e Regras de Negócio

---

### Users (Usuários)

---

#### `POST /users` — Criar Usuário

**Controller:** `CreateUserController.create()`  
**Arquivo:** `src/controllers/users/CreateUserController.ts`

**Entrada (Body JSON):**
```json
{
  "name": "string",
  "email": "string",
  "password": "string"
}
```

**Regras de Negócio:**
1. Verifica se já existe um usuário com o e-mail informado (`findUnique` por email).
2. **Se o e-mail já estiver cadastrado:** retorna `400` com a mensagem `"This email has been already registered."`.
3. Gera o hash da senha com `bcryptjs` usando **salt 10**.
4. Cria o registro no banco com a senha hasheada.
5. **Retorna o objeto `user` completo** (incluindo o hash da senha — potencial ponto de melhoria na migração).

**Respostas:**
| Status | Situação |
|--------|---------|
| `200` | Usuário criado. Body: `{ user: { id, name, email, password } }` |
| `400` | E-mail já cadastrado. Body: `"This email has been already registered."` |

> ⚠️ **Bug identificado:** Quando o e-mail já existe, o código faz `res.status(400).send(...)` mas **não retorna** (`return`). A execução continua e tenta criar o usuário duplicate, causando erro não tratado. Na migração, adicionar `return` nesse ponto.

---

#### `POST /users/auth` — Autenticar Usuário (Login)

**Controller:** `AuthUserController.authenticate()`  
**Arquivo:** `src/controllers/users/AuthUserController.ts`

**Entrada (Body JSON):**
```json
{
  "email": "string",
  "password": "string"
}
```

**Regras de Negócio:**
1. Busca o usuário pelo e-mail (`findUnique`).
2. **Se não encontrar:** retorna `404` com `"User not found."`.
3. Compara a senha fornecida com o hash armazenado via `bcryptjs.compare`.
4. **Se a senha não bater:** retorna `400` com `"Invalid password."`.
5. Verifica se a variável de ambiente `SECRET` está definida. Se não, lança exceção.
6. Gera um **JWT** assinado com `{ id: user.id }` e expiração de **1 dia** (`expiresIn: "1d"`).
7. Retorna `id`, `email` do usuário e o `token`.

**Respostas:**
| Status | Situação |
|--------|---------|
| `200` | Login OK. Body: `{ user: { id, email }, token: "jwt_string" }` |
| `400` | Senha inválida. Body: `"Invalid password."` |
| `404` | Usuário não encontrado. Body: `"User not found."` |
| `500` | `SECRET` não definida (erro não tratado — lança exceção) |

---

#### `GET /users` — Listar Usuários

**Controller:** `GetUserController.showUsers()`  
**Arquivo:** `src/controllers/users/GetUserController.ts`  
**Middleware:** `AuthMiddleware` (JWT obrigatório)

**Regras de Negócio:**
1. Requer token JWT válido no header `Authorization: Bearer <token>`.
2. Busca todos os usuários no banco (`findMany`) sem nenhum filtro.
3. Retorna lista completa, **incluindo os hashes de senha** (potencial falha de segurança — recomendar remoção do campo `password` na resposta na migração).

**Respostas:**
| Status | Situação |
|--------|---------|
| `200` | Body: `{ user: [ ...todos os usuários ] }` |
| `401` | Token não fornecido ou inválido |

---

### Vacancies (Vagas)

---

#### `POST /vacancies` — Criar Vaga

**Controller:** `CreateVacancyController.create()`  
**Arquivo:** `src/controllers/vacancies/CreateVacancyController.ts`

**Entrada (Body JSON):**
```json
{
  "titulo": "string",
  "status": "string",
  "qtdeVagas": "number",
  "descricao": "string",
  "setor": "string",
  "senioridade": "string",
  "diversidade": "string",
  "pcd": "string",
  "salario": "string",
  "contrato": "string",
  "turno": "string",
  "local": "string",
  "endereco": "string",
  "dataAbertura": "string",
  "dataFechamento": "string"
}
```

**Regras de Negócio:**
1. Verifica se `req.body` existe; se não, retorna `400`.
2. Não há validações de campos individuais ou unicidade.
3. Cria a vaga diretamente no banco com todos os campos.
4. Em caso de erro do Prisma, retorna `500`.

**Respostas:**
| Status | Situação |
|--------|---------|
| `200` | Body: `{ vacancy: { ...dados da vaga criada } }` |
| `400` | Body vazio. Body: `{ msg: "Please provide all informations." }` |
| `500` | Erro interno. Body: `{ msg: "An error occurred" }` |

---

#### `GET /vacancies` — Listar Todas as Vagas

**Controller:** `GetVacancyController.get()`  
**Arquivo:** `src/controllers/vacancies/GetVacancyControlller.ts`

**Regras de Negócio:**
1. Busca todas as vagas no banco (`findMany`) sem filtros, ordenação ou paginação.
2. Retorna lista completa.

**Respostas:**
| Status | Situação |
|--------|---------|
| `200` | Body: `{ vacancies: [ ...todas as vagas ] }` |

---

#### `GET /vacancies/:vacancyID` — Detalhes de Uma Vaga

**Controller:** `GetVacancyInfoController.get()`  
**Arquivo:** `src/controllers/vacancies/GetVacancyInfoController.ts`

**Parâmetros de Rota:**
| Param | Tipo | Descrição |
|-------|------|-----------|
| `vacancyID` | `string → Int` | ID da vaga (convertido para número) |

**Regras de Negócio:**
1. Converte `vacancyID` de string para `Int`.
2. Busca a vaga pelo ID (`findUnique`).
3. **Não verifica se a vaga existe** — retorna `null` em `vacancy` se não encontrar.
4. Em caso de erro do Prisma, retorna `400`.

**Respostas:**
| Status | Situação |
|--------|---------|
| `200` | Body: `{ vacancy: { ...dados } }` (pode ser `null` se não existir) |
| `400` | Erro ao buscar. Body: `{ msg: "an error occured" }` |

---

#### `PUT /vacancies/:vacancyID` — Atualizar Vaga

**Controller:** `UpdateVacancyController.update()`  
**Arquivo:** `src/controllers/vacancies/UpdateVacancyController.ts`

**Parâmetros de Rota:**
| Param | Tipo | Descrição |
|-------|------|-----------|
| `vacancyID` | `string → Int` | ID da vaga a ser atualizada |

**Entrada (Body JSON):** mesmos campos do `POST /vacancies`.

**Regras de Negócio:**
1. Verifica se `req.body` e `req.params` existem; se não, retorna `400`.
2. Converte `vacancyID` para número.
3. Faz update de **todos os campos** (não é PATCH parcial — todos os campos são sobrescritos com os valores do body).
4. Se a vaga não existir, o Prisma lança exceção capturada pelo `catch`, retornando `500`.

**Respostas:**
| Status | Situação |
|--------|---------|
| `200` | Body: `{ vacancy: { ...dados atualizados } }` |
| `400` | Body/params ausentes. Body: `{ msg: "Please provide all informations." }` |
| `500` | Erro (ex.: vaga não encontrada). Body: `{ msg: "An error occurred" }` |

---

#### `DELETE /vacancies/:vacancyID` — Deletar Vaga

**Controller:** `DeleteVacancyController.delete()`  
**Arquivo:** `src/controllers/vacancies/DeleteVacancyController.ts`

**Parâmetros de Rota:**
| Param | Tipo | Descrição |
|-------|------|-----------|
| `vacancyID` | `string → Int` | ID da vaga a ser deletada |

**Regras de Negócio:**
1. Converte `vacancyID` para número.
2. Executa `prisma.vacancy.delete()` diretamente.
3. **Nota:** A verificação `if (!vacancy)` após o delete nunca é atingida, pois se a vaga não existir, o Prisma lança exceção antes.
4. Se a vaga não existir, o `catch` retorna `500` com o objeto de erro.

**Respostas:**
| Status | Situação |
|--------|---------|
| `200` | Body: `{ msg: "Vacancy has been deleted" }` |
| `500` | Erro (ex.: vaga não encontrada). Body: `{ error: <objeto do erro> }` |

---

### Applications (Candidaturas)

---

#### `POST /applications/:vacancyID` — Candidatar-se a uma Vaga

**Controller:** `CreateApplicationController.apply()`  
**Arquivo:** `src/controllers/applications/CreateApplicationController.ts`

**Parâmetros de Rota:**
| Param | Tipo | Descrição |
|-------|------|-----------|
| `vacancyID` | `string → Int` | ID da vaga para candidatura |

**Entrada (Body JSON):**
```json
{
  "nomeCompleto": "string",
  "email": "string",
  "telefone": "string",
  "dataNasc": "string",
  "cpf": "string",
  "resumoProfissional": "string",
  "cargo": "string",
  "empresa": "string",
  "dataInicioEmpresa": "string",
  "dataTerminoEmpresa": "string",
  "descricaoATVD": "string",
  "situacao": "string",
  "escolaridade": "string",
  "curso": "string",
  "instituicao": "string",
  "dataInicioEstudo": "string",
  "dataTerminoEstudos": "string"
}
```

**Regras de Negócio:**
1. Extrai `vacancyID` da rota e converte para `Int`.
2. **Verificação de duplicidade:** Busca se já existe uma `Application` com o mesmo `email` **OU** o mesmo `cpf` (condição `OR`).
3. **Se já existe candidatura com esse e-mail ou CPF:** retorna `400` com `"Already applied to this job vacancy"`.
   - ⚠️ **Atenção:** A verificação é global (não por vaga). Um candidato não pode se candidatar a **nenhuma** vaga se já se candidatou a qualquer outra. Isso se deve ao `email` e `cpf` serem campos `UNIQUE` no banco.
4. Cria a candidatura associando ao `vacancyID`.
5. **Não verifica se a vaga existe** antes de criar a candidatura.

**Respostas:**
| Status | Situação |
|--------|---------|
| `200` | Body: `{ application: { ...dados da candidatura } }` |
| `400` | Candidato já cadastrado. Body: `{ msg: "Already applied to this job vacancy" }` |
| `500` | Erro interno. Body: `{ error: <objeto do erro> }` |

---

#### `GET /applications/:vacancyID` — Candidaturas de Uma Vaga

**Controller:** `GetApplicationsController.get()`  
**Arquivo:** `src/controllers/applications/GetApplicationsController.ts`

**Parâmetros de Rota:**
| Param | Tipo | Descrição |
|-------|------|-----------|
| `vacancyID` | `string → Int` | ID da vaga |

**Regras de Negócio:**
1. Converte `vacancyID` para número.
2. Busca todas as candidaturas onde `vacancyID` corresponde.
3. **Se a lista estiver vazia:** retorna `200` com mensagem `"There is no applications for this position"`.
4. Se houver candidaturas, retorna a lista.

**Respostas:**
| Status | Situação |
|--------|---------|
| `200` | Com candidaturas: `{ applications: [ ... ] }` |
| `200` | Sem candidaturas: `{ msg: "There is no applications for this position" }` |

---

#### `GET /applications/` — Listar Todas as Candidaturas

**Controller:** `GetAllApplicationsController.get()`  
**Arquivo:** `src/controllers/applications/GetAllAplicationsController.ts`

**Regras de Negócio:**
1. Busca todas as candidaturas (`findMany`) sem filtros.
2. **Se não houver nenhuma candidatura:** retorna `404` com `{ error: "There are no applications" }`.
3. Se houver, retorna a lista.

**Respostas:**
| Status | Situação |
|--------|---------|
| `200` | Body: `{ applications: [ ...todas ] }` |
| `404` | Sem candidaturas. Body: `{ error: "There are no applications" }` |

---

### Statistics (Estatísticas)

---

#### `GET /statistics` — Estatísticas Gerais

**Controller:** `GetStatisticsController.get()`  
**Arquivo:** `src/controllers/statistics/getStatisticsController.ts`

**Regras de Negócio:**
1. Busca **todas as vagas** e **todas as candidaturas** do banco.
2. **Calcula o tempo médio de duração das vagas** (em dias):
   - Para cada vaga: `(dataFechamento - dataAbertura)` em milissegundos → converte para dias.
   - Soma todos os tempos → divide pelo total de vagas.
   - ⚠️ Se não houver vagas, `tempoMedio` será `NaN` (divisão por zero).
3. **Agrupa vagas por mês de abertura** (`dataAbertura.getMonth() + 1`):
   - Retorna array `vagasPorMes` com 12 posições (Janeiro a Dezembro), cada uma com a contagem de vagas abertas naquele mês.
4. **Agrupa vagas por setor** (14 setores fixos):
   - Usa `Array.filter` + `String.includes` para cada setor.
   - Retorna array `VagasPorSetor` com contagem e cor CSS para cada setor.
5. Retorna tudo em um único objeto.

**Resposta (200):**
```json
{
  "tempoMedio": 30.5,
  "vacancies": [ ...todas as vagas ],
  "VagasPorSetor": [
    { "setor": "Administrativo", "vagas": 3, "fill": "var(--color-Administrativo)" },
    ...
  ],
  "vagasPorMes": [
    { "mes": "Janeiro", "Vagas": 2 },
    { "mes": "Fevereiro", "Vagas": 1 },
    ...
  ],
  "candidates": [ ...todas as candidaturas ]
}
```

**Setores contemplados no agrupamento:**

| Setor | Chave no filtro |
|-------|----------------|
| Administrativo | `"Administrativo"` |
| Financeiro | `"Financeiro"` |
| Comercial | `"Comercial"` |
| Vendas | `"Vendas"` |
| Marketing | `"Marketing"` |
| Tecnologia da Informação | `"Tecnologia da Informação"` |
| Atendimento ao Cliente | `"Atendimento ao Cliente"` |
| Logística | `"Logística"` |
| Jurídico | `"Jurídico"` |
| Produção / Manufatura | `"Produção / Manufatura"` |
| Compras / Suprimentos | `"Compras / Suprimentos"` |
| Almoxarifado | `"Almoxarifado"` |
| Qualidade | `"Qualidade"` |
| Segurança do Trabalho | `"Segurança do Trabalho"` |

---

## Middleware de Autenticação

**Arquivo:** `src/middleware/auth.ts`  
**Função:** `AuthMiddleware`

**Funcionamento:**
1. Lê o header `Authorization` da requisição.
2. **Se não houver header:** retorna `401` com `"Token not provided."`.
3. Divide o valor pelo espaço: espera formato `Bearer <token>`.
4. Verifica o token JWT usando a chave `SECRET` do `.env`.
5. **Se o token for inválido ou expirado:** retorna `401` com `"Token invalid"`.
6. Se válido, extrai o `id` do payload e o injeta em `req.userId`.
7. Chama `next()` para continuar para o controller.

**Rotas protegidas:** apenas `GET /users`.

---

## Fluxos de Autenticação

### Fluxo de Login
```
Cliente → POST /users/auth { email, password }
       ← 200 { user: { id, email }, token: "jwt..." }
```

### Fluxo de Acesso Protegido
```
Cliente → GET /users
          Header: Authorization: Bearer <token>
        → AuthMiddleware valida JWT
        → Controller executa
        ← 200 { user: [...] }
```

### Expiração de Token
- Tokens expiram em **1 dia** (`expiresIn: "1d"`).
- Após expiração, qualquer rota protegida retorna `401 "Token invalid"`.

---

## Códigos de Resposta HTTP

| Código | Significado no Projeto |
|--------|----------------------|
| `200` | Operação realizada com sucesso |
| `400` | Dados inválidos, duplicados ou faltantes |
| `401` | Token ausente ou inválido |
| `404` | Recurso não encontrado (apenas em `GetAllApplications`) |
| `500` | Erro interno do servidor / erro do Prisma |

---

## Observações para Migração

### Bugs e Inconsistências a Corrigir

| # | Localização | Problema | Recomendação |
|---|-----------|---------|-------------|
| 1 | `CreateUserController` | Falta `return` após `res.status(400).send(...)` quando e-mail já existe | Adicionar `return` antes ou usar early return |
| 2 | `DeleteVacancyController` | `if (!vacancy)` após `.delete()` é código morto — Prisma lança exceção antes | Remover verificação ou tratar o erro específico |
| 3 | `GetVacancyInfoController` | Retorna `{ vacancy: null }` com status 200 quando a vaga não existe | Verificar se é `null` e retornar `404` |
| 4 | `CreateUserController` / `GetUserController` | Retorna o hash da senha nas respostas | Omitir o campo `password` nas respostas |
| 5 | `GetStatisticsController` | `tempoMedio` é `NaN` se não houver vagas (divisão por zero) | Verificar `vacancies.length > 0` antes |
| 6 | `CreateApplicationController` | A unicidade de e-mail/CPF é global — um candidato não pode se candidatar a múltiplas vagas | Reavaliar regra de negócio: verificar duplicidade **por vaga** |
| 7 | Todas as rotas de vagas | Não há autenticação nas rotas de criação/edição/deleção de vagas | Adicionar `AuthMiddleware` nas rotas admin |

### Estrutura de Pastas para Referência

```
src/
├── index.ts                        # Ponto de entrada, config Express/CORS
├── Routes/
│   └── Routes.ts                   # Registro de todas as rotas
├── controllers/
│   ├── users/
│   │   ├── CreateUserController.ts
│   │   ├── AuthUserController.ts
│   │   └── GetUserController.ts
│   ├── vacancies/
│   │   ├── CreateVacancyController.ts
│   │   ├── GetVacancyControlller.ts   # (typo: 3 "l")
│   │   ├── GetVacancyInfoController.ts
│   │   ├── UpdateVacancyController.ts
│   │   └── DeleteVacancyController.ts
│   ├── applications/
│   │   ├── CreateApplicationController.ts
│   │   ├── GetApplicationsController.ts
│   │   └── GetAllAplicationsController.ts
│   └── statistics/
│       └── getStatisticsController.ts
├── middleware/
│   └── auth.ts                     # JWT AuthMiddleware
└── utils/
    └── prisma.ts                   # Singleton PrismaClient

prisma/
├── schema.prisma                   # Modelos: User, Vacancy, Application
└── dev.db                          # Banco SQLite (desenvolvimento)
```

### Padrão Arquitetural Atual

O projeto segue uma arquitetura **simples e flat** (sem separação de camadas Use Case / Repository / Service). Toda a lógica de negócio, acesso ao banco e resposta HTTP estão no mesmo arquivo de Controller. Ao migrar, considerar separar em:

- **Controller** → apenas trata req/res HTTP
- **CQRS** → Cada rota deve ter seu próprio controller, command e query e seus respectivos handlers
- **Repository** → cada entidade deve ter seu próprio repository, podendo ser acessado pelo unit of work
- **Unit of Work** → responsável por gerenciar as transações e o ciclo de vida dos repositories
- **Domain** → responsável por gerenciar as entidades e suas regras de negócio
- **Infra** → responsável por gerenciar as dependências externas, como banco de dados, serviços externos, etc
