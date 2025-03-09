import { Request, Response } from "express";
import { prisma } from "../utils/prisma";

export class CreateApplicationController {
  async apply(req: Request, res: Response):Promise<Response> {
    const {
      vacancyID,
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
      instituicao,
      dataInicioEstudo,
      dataTerminoEstudos,
    } = req.body;

    try {
      
      const isAlreadyApplied = await prisma.application.findFirst({
        where: {
          OR:[{email}, {cpf}],
        }
      })

      if(isAlreadyApplied){
        return res.status(400).json({msg:"Already applied to this job vacancy"});
      }
  
      const application = await prisma.application.create({
        data: {
        vacancyID,  
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
        instituicao,
        dataInicioEstudo,
        dataTerminoEstudos,
        },
      });
  
      return res.status(200).json({application:application});

    } catch (error) {
      console.log(error)
      return res.status(500).json({error: error})
    }

  }
}
