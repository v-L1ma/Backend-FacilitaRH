import { Router } from "express";
import { CreateUserController } from "../controller/CreateUserController";
import { GetUserController } from "../controller/GetUserController";
import { AuthUserController } from "../controller/AuthUserController";
import { AuthMiddleware } from "../middleware/auth";
import { CreateApplicationController } from "../controller/CreateApplicationController";
import { GetApplicationsController } from "../controller/GetApplicationsController";

const createUserController = new CreateUserController();
const getUserController = new GetUserController();
const authUserController = new AuthUserController();
const createApplicationController = new CreateApplicationController();
const getApplicationsController = new GetApplicationsController();

export const router = Router();

router.post("/create", (req, res) => {createUserController.create(req, res)});

router.get("/users", AuthMiddleware, (req, res) => {getUserController.showUsers(req, res)});

router.post("/users/auth", (req, res) => {authUserController.authenticate(req, res)});

router.post("/apply", (req,res)=>{createApplicationController.apply(req,res)});

router.get("/applications", (req,res)=>{getApplicationsController.get(req,res)});