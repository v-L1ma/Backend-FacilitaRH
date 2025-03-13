import { compare } from "bcryptjs";
import { prisma } from "../../utils/prisma"; 
import { Request, Response } from "express";
import { sign } from "jsonwebtoken";
import dotenv from "dotenv";

dotenv.config();

export class AuthUserController {
  async authenticate(req: Request, res: Response): Promise<Response>{
    const { email, password } = req.body;

    const user = await prisma.user.findUnique({
      where: { email },
    });

    if (!user) {
      return res.status(404).send("User not found.");
    }

    const isMatchPassword = await compare(password, user.password);

    if (!isMatchPassword) {
      return res.status(400).send("Invalid password.");
    }

    const SECRET = process.env.SECRET;

    if (!SECRET) {
      throw new Error("SECRET KEY não está definida");
    }

    const token = sign({ id: user.id }, SECRET, { expiresIn: "1d" });

    const { id } = user;

    return res.status(200).send({ user: { id, email }, token });
  }
}
