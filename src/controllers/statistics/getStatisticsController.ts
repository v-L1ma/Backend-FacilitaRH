import { Request, Response } from "express";
import { prisma } from "../../utils/prisma";

export class GetStatisticsController {
  async get(req: Request, res: Response) {
    const vacancies = await prisma.vacancy.findMany();

    const vacanciesOpen: number = vacancies.length;

    let tempoMedio: number;
    let tempoTotal:number = 0;
    let vagaEmAtraso:number = 0;

    for (let i = 0; i < vacancies.length; i++) {
      const dataAbertura: string = vacancies[i].dataAbertura;
      const dataFechamento: string = vacancies[i].dataFechamento;

      let anos:number,meses:number,dias:number;

      const [diaAbertura, mesAbertura, anoAbertura] = dataAbertura.split("-");
      const [diaFechamento, mesFechamento, anoFechamento] =
        dataFechamento.split("-");

      if (Number(anoAbertura) > Number(anoFechamento)) {
        anos = Number(anoAbertura) - Number(anoFechamento);
      } else {
        anos = Number(anoFechamento) - Number(anoAbertura);
      }

      if (Number(mesAbertura) > Number(mesFechamento)) {
        meses = Number(mesAbertura) - Number(mesFechamento);
      } else {
        meses = Number(mesFechamento) - Number(mesAbertura);
      }

      if (Number(diaAbertura) > Number(diaFechamento)) {
        dias = Number(diaAbertura) - Number(diaFechamento);
      } else {
        dias = Number(diaFechamento) - Number(diaAbertura);
      }

      const tempoDessaVaga = dias+(meses*30)+(anos*365);

      tempoTotal = tempoTotal+tempoDessaVaga;

      if(tempoDessaVaga>30){
        vagaEmAtraso++
      }

    }

    tempoMedio=tempoTotal/vacancies.length;

    return res.status(200).json({tempoMedio:tempoMedio,vacanciesOpen:vacanciesOpen,vagaEmAtraso:vagaEmAtraso})
  }
}
