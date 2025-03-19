import { Request, Response } from "express";
import { prisma } from "../../utils/prisma";

export class GetStatisticsController {
  async get(req: Request, res: Response) {
    const vacancies = await prisma.vacancy.findMany();

    const candidates = await prisma.application.findMany();

    let tempoMedio: number;
    let tempoTotal: number = 0;
    let vagaEmAtraso: number = 0;

    for (let i = 0; i < vacancies.length; i++) {
      const dataAbertura = new Date(vacancies[i].dataAbertura);
      const dataFechamento = new Date(vacancies[i].dataFechamento);

      const diferencaMs = dataFechamento.getTime() - dataAbertura.getTime();

      const tempoDessaVaga = diferencaMs / (1000 * 60 * 60 * 24);

      tempoTotal += tempoDessaVaga;

    }

    tempoMedio = tempoTotal / vacancies.length;

    return res
      .status(200)
      .json({
        tempoMedio: tempoMedio,
        vacancies: vacancies,
      });
  }
}
