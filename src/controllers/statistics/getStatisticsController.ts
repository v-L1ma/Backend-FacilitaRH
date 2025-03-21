import { Request, Response } from "express";
import { prisma } from "../../utils/prisma";

export class GetStatisticsController {
  async get(req: Request, res: Response) {
    const vacancies = await prisma.vacancy.findMany();

    const candidates = await prisma.application.findMany();

    let tempoMedio: number;
    let tempoTotal: number = 0;
    let janeiro: number = 0;
    let fevereiro: number = 0;
    let marco: number = 0;
    let abril: number = 0;
    let maio: number = 0;
    let junho: number = 0;
    let julho: number = 0;
    let agosto: number = 0;
    let setembro: number = 0;
    let outubro: number = 0;
    let novembro: number = 0;
    let dezembro: number = 0;

    for (let i = 0; i < vacancies.length; i++) {
      const dataAbertura = new Date(vacancies[i].dataAbertura);
      const dataFechamento = new Date(vacancies[i].dataFechamento);

      const diferencaMs = dataFechamento.getTime() - dataAbertura.getTime();

      const tempoDessaVaga = diferencaMs / (1000 * 60 * 60 * 24);

      tempoTotal += tempoDessaVaga;

      switch (dataAbertura.getMonth() + 1) {
        case 1:
          janeiro++;
          break;

        case 2:
          fevereiro++;
          break;

        case 3:
          marco++;
          break;

        case 4:
          abril++;
          break;

        case 5:
          maio++;
          break;

        case 6:
          junho++;
          break;

        case 7:
          julho++;
          break;

        case 8:
          agosto++;
          break;

        case 9:
          setembro++;
          break;

        case 10:
          outubro++;
          break;

        case 11:
          novembro++;
          break;

        case 12:
          dezembro++;
          break;

        default:
          break;
      }
    }

    tempoMedio = tempoTotal / vacancies.length;

    const vagasPorMes = [
      { mes: "Janeiro", Vagas: janeiro },
      { mes: "Fevereiro", Vagas: fevereiro },
      { mes: "Marco", Vagas: marco },
      { mes: "Abril", Vagas: abril },
      { mes: "Maio", Vagas: maio },
      { mes: "Junho", Vagas: junho },
      { mes: "Julho", Vagas: julho },
      { mes: "Agosto", Vagas: agosto },
      { mes: "Setembro", Vagas: setembro },
      { mes: "Outubro", Vagas: outubro },
      { mes: "Novembro", Vagas: novembro },
      { mes: "Dezembro", Vagas: dezembro },
    ];

    const administrativo = vacancies.filter((vaga) =>
      vaga.setor.includes("Administrativo")
    );
    const financeiro = vacancies.filter((vaga) =>
      vaga.setor.includes("Financeiro")
    );
    const comercial = vacancies.filter((vaga) =>
      vaga.setor.includes("Comercial")
    );
    const vendas = vacancies.filter((vaga) => vaga.setor.includes("Vendas"));
    const marketing = vacancies.filter((vaga) =>
      vaga.setor.includes("Marketing")
    );
    const tecnologiaDaInformacao = vacancies.filter((vaga) =>
      vaga.setor.includes("Tecnologia da Informação")
    );
    const atendimentoAoCliente = vacancies.filter((vaga) =>
      vaga.setor.includes("Atendimento ao Cliente")
    );
    const logistica = vacancies.filter((vaga) =>
      vaga.setor.includes("Logística")
    );
    const juridico = vacancies.filter((vaga) =>
      vaga.setor.includes("Jurídico")
    );
    const producaoManufatura = vacancies.filter((vaga) =>
      vaga.setor.includes("Produção / Manufatura")
    );
    const comprasSuprimentos = vacancies.filter((vaga) =>
      vaga.setor.includes("Compras / Suprimentos")
    );
    const almoxarifado = vacancies.filter((vaga) =>
      vaga.setor.includes("Almoxarifado")
    );
    const qualidade = vacancies.filter((vaga) =>
      vaga.setor.includes("Qualidade")
    );
    const segurancaDoTrabalho = vacancies.filter((vaga) =>
      vaga.setor.includes("Segurança do Trabalho")
    );

    const VagasPorSetor = [
      {
        setor: "Administrativo",
        vagas: administrativo.length,
        fill: "var(--color-Administrativo)",
      },
      {
        setor: "Financeiro",
        vagas: financeiro.length,
        fill: "var(--color-Financeiro)",
      },
      {
        setor: "Comercial",
        vagas: comercial.length,
        fill: "var(--color-Comercial)",
      },
      { setor: "Vendas", vagas: vendas.length, fill: "var(--color-Vendas)" },
      {
        setor: "Marketing",
        vagas: marketing.length,
        fill: "var(--color-Marketing)",
      },
      {
        setor: "Tecnologia da Informação",
        vagas: tecnologiaDaInformacao.length,
        fill: "var(--color-TI)",
      },
      {
        setor: "Atendimento ao Cliente",
        vagas: atendimentoAoCliente.length,
        fill: "var(--color-AtendimentoAoCliente)",
      },
      {
        setor: "Logística",
        vagas: logistica.length,
        fill: "var(--color-Logistica)",
      },
      {
        setor: "Jurídico",
        vagas: juridico.length,
        fill: "var(--color-Juridico)",
      },
      {
        setor: "Produção / Manufatura",
        vagas: producaoManufatura.length,
        fill: "var(--color-ProducaoManufatura)",
      },
      {
        setor: "Compras / Suprimentos",
        vagas: comprasSuprimentos.length,
        fill: "var(--color-ComprasSuprimentos)",
      },
      {
        setor: "Almoxarifado",
        vagas: almoxarifado.length,
        fill: "var(--color-Almoxarifado)",
      },
      {
        setor: "Qualidade",
        vagas: qualidade.length,
        fill: "var(--color-Qualidade)",
      },
      {
        setor: "Segurança do Trabalho",
        vagas: segurancaDoTrabalho.length,
        fill: "var(--color-SegurancaDoTrabalho)",
      },
    ];

    return res.status(200).json({
      tempoMedio: tempoMedio,
      vacancies: vacancies,
      VagasPorSetor: VagasPorSetor,
      vagasPorMes: vagasPorMes,
    });
  }
}
