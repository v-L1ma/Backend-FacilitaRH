import { hash } from "bcryptjs";
import { prisma } from "../utils/prisma";
import { Request, Response } from "express";

export class CreateUserController {
    async create(req:Request, res: Response):Promise<Response>{
        const {name, email , password} = req.body;

        

        const isUserRegistered = await prisma.user.findUnique({
            where: {email}
        })

        if(isUserRegistered){
            res.status(400).send("This email has been already registered.");
        }

        const hashPassword = await hash(password, 10);

        const user = await prisma.user.create({
            data: {
                name,
                email,
                password: hashPassword,
            }
        });

        return res.status(200).json({user: user})
    }
}