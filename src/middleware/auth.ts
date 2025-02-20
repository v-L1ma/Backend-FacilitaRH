import { NextFunction, Request, Response } from "express";
import { verify } from "jsonwebtoken";

type TokenPayLoad = {
  id: string;
  iat: number;
  exp: number;
};

export function AuthMiddleware(
  req: Request,
  res: Response,
  next: NextFunction
):any {
  const { authorization } = req.headers;

  if (!authorization) {
    return res.status(401).send("Token not provided.");
  }

  const [bearer, token] = authorization.split(" ");

  try {
    const SECRET = process.env.SECRET;

    if (!SECRET) {
      throw new Error("SECRET KEY is not defined.");
    }

    const decoded = verify(token, SECRET);
    const { id } = decoded as TokenPayLoad;

    req.userId = id;
    next();
  } catch (error) {
    return res.status(401).send("Token invalid");
  }
}
