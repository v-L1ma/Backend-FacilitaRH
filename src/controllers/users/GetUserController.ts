import { prisma } from  "../../utils/prisma";
import { Request, Response } from "express";

export class GetUserController {
    async showUsers(req:Request, res: Response):Promise<Response>{
        const user = await prisma.user.findMany();

        return res.status(200).json({user: user})
    }
}