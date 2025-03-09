import { Request, Response } from "express";
import { prisma } from "../utils/prisma";

export class CreateVacancyController{
    async create(req:Request, res:Response):Promise<Response>{

        const { 
            titulo,
            qtdeVagas,
            descricao,
            setor,
            senioridade,
            diversidade,
            pcd,
            salario,
            contrato,
            turno,
            local,
            endereco} = req.body;

        const vacancy = await prisma.vacancy.create({
            data: {
                titulo,
                qtdeVagas,
                descricao,
                setor,
                senioridade,
                diversidade,
                pcd,
                salario,
                contrato,
                turno,
                local,
                endereco
            }
        });

        return res.status(200).json({vacancy:vacancy});

    }
}