import { Request, Response } from "express";
import { prisma } from "../../utils/prisma";

export class GetApplicationsController{

    async get(req:Request,res:Response):Promise<Response>{

        const {vacancyID} = req.params;
        const id = Number(vacancyID)

        const applications = await prisma.application.findMany({
            where: {
                vacancyID: id,
            }
        });

        if(applications.length == 0){
            return res.status(200).json({msg:"There is no applications for this position"});
        }

        return res.status(200).json({applications:applications});

    }

}