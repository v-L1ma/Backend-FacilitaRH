import { Request, Response } from "express";
import { prisma } from  "../../utils/prisma";

export class GetVacancyController{

    async get(req:Request,res:Response){
        const vacancies = await prisma.vacancy.findMany();

        return res.status(200).json({vacancies:vacancies});
    
    }

}