import { prisma } from "../utils/prisma";
import { Request, Response } from "express";

export class UserController {
    async create(req:Request, res: Response):Promise<any>{
        const {name, email , password} = req.body;
        const user = await prisma.user.create({
            data: {
                name,
                email,
                password,
            }
        });

        return res.status(201).json({user: user})
    }
}