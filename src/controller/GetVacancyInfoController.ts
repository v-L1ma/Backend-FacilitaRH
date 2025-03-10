import { Request, Response } from "express";
import { prisma } from "../utils/prisma";

export class GetVacancyInfoController{

    async get(req:Request,res:Response):Promise<Response>{

        const {vacancyID} = req.params;
        
        try{
            const vacancy = await prisma.vacancy.findUnique({
                where: {id: parseInt(vacancyID)},
            })

            return res.status(200).json({vacancy: vacancy});

        } catch(error){
            return res.status(400).json({msg:"an error occured"});
        }

       
    }

}