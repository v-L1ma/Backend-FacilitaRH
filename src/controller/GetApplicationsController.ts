import { Request, Response } from "express";
import { prisma } from "../utils/prisma";

export class GetApplicationsController{

    async get(req:Request,res:Response):Promise<Response>{

        const applications = await prisma.application.findMany();

        return res.status(200).json({applications:applications});

    }

}