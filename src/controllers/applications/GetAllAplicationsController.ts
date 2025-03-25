import { Request, Response } from "express";
import { prisma } from "../../utils/prisma";

export class GetAllApplicationsController {
    async get(req: Request, res: Response): Promise<Response> {
        const applications = await prisma.application.findMany();

        if (applications.length === 0) {
            return res.status(404).json({ error: "There are no applications" });
        }

        return res.status(200).json({ applications: applications });
    }
}