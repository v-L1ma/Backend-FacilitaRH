import { Request, Response } from "express";
import { prisma } from  "../../utils/prisma";

export class CreateVacancyController{
    async create(req:Request, res:Response):Promise<Response>{

        if(!req.body){
            return res.status(400).json({msg: "Please provide all informations."})
        }

        const { 
            titulo,
            status,
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
            endereco,
            dataAbertura,
            dataFechamento
        } = req.body;

    try {

        const vacancy = await prisma.vacancy.create({
            data: {
                titulo,
                status,
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
                endereco,
                dataAbertura,
                dataFechamento
            }
        });

        return res.status(200).json({vacancy:vacancy});
        
    } catch (error) {
        console.log(error)
        return res.status(500).json({msg: "An error occurred"})
        
    }

       

    }
}