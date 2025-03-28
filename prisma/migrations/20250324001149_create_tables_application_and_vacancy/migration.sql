-- CreateTable
CREATE TABLE "Application" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "vacancyID" INTEGER NOT NULL,
    "nomeCompleto" TEXT NOT NULL,
    "email" TEXT NOT NULL,
    "telefone" TEXT NOT NULL,
    "dataNasc" TEXT NOT NULL,
    "cpf" TEXT NOT NULL,
    "resumoProfissional" TEXT NOT NULL,
    "cargo" TEXT NOT NULL,
    "empresa" TEXT NOT NULL,
    "dataInicioEmpresa" TEXT NOT NULL,
    "dataTerminoEmpresa" TEXT NOT NULL,
    "descricaoATVD" TEXT NOT NULL,
    "situacao" TEXT NOT NULL,
    "escolaridade" TEXT NOT NULL,
    "curso" TEXT NOT NULL,
    "instituicao" TEXT NOT NULL,
    "dataInicioEstudo" TEXT NOT NULL,
    "dataTerminoEstudos" TEXT NOT NULL
);

-- CreateTable
CREATE TABLE "Vacancy" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "status" TEXT NOT NULL,
    "titulo" TEXT NOT NULL,
    "qtdeVagas" INTEGER NOT NULL,
    "descricao" TEXT NOT NULL,
    "setor" TEXT NOT NULL,
    "senioridade" TEXT NOT NULL,
    "diversidade" TEXT NOT NULL,
    "pcd" TEXT NOT NULL,
    "salario" TEXT NOT NULL,
    "contrato" TEXT NOT NULL,
    "turno" TEXT NOT NULL,
    "local" TEXT NOT NULL,
    "endereco" TEXT NOT NULL,
    "dataAbertura" TEXT NOT NULL,
    "dataFechamento" TEXT NOT NULL
);

-- CreateIndex
CREATE UNIQUE INDEX "Application_email_key" ON "Application"("email");

-- CreateIndex
CREATE UNIQUE INDEX "Application_cpf_key" ON "Application"("cpf");
