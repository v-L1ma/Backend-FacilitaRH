import { Request, Response } from "express";
import { prisma } from "../utils/prisma";

export class CreateApplicationController {
  async apply(req: Request, res: Response):Promise<Response> {
    const {
      nomeCompleto,
      email,
      telefone,
      dataNasc,
      cpf,
      cargo,
      empresa,
      dataInicioEmpresa,
      dataTerminoEmpresa,
      descricaoATVD,
      situacao,
      escolaridade,
      curso,
      instituição,
      dataInicioEstudo,
      dataTerminoEstudos,
    } = req.body;

    const application = await prisma.application.create({
      data: {
      nomeCompleto,
      email,
      telefone,
      dataNasc,
      cpf,
      cargo,
      empresa,
      dataInicioEmpresa,
      dataTerminoEmpresa,
      descricaoATVD,
      situacao,
      escolaridade,
      curso,
      instituição,
      dataInicioEstudo,
      dataTerminoEstudos,
      },
    });

    return res.status(200).json({application:application});
  }
}
