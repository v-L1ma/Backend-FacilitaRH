import { Request, Response } from "express";
import { prisma } from "../utils/prisma";

export class DeleteVacancyController {
  async delete(req: Request, res: Response): Promise<Response> {
    const { vacancyID } = req.params;

    try {
      const vacancy = await prisma.vacancy.delete({
        where: { id: Number(vacancyID) },
      });

      if (!vacancy) {
        return res
          .status(400)
          .json({ msg: "The selected vacancy does not exists." });
      }

      return res.status(200).json({ msg: "Vacancy has been deleted" });
    } catch (error) {
      console.log(error);
      return res.status(500).json({ error: error });
    }
  }
}
