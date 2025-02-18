import { Router } from "express";
import { UserController } from "../controller/UserController";

const userController = new UserController();

export const router = Router();

router.post("/create", userController.create);

/*
router.get("/", (req,res) =>{
    return res.json({msg: "Hello, world"});
})*/ 
