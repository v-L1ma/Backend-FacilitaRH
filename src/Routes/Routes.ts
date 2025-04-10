import { Router } from "express";
import { CreateUserController } from "../controllers/users/CreateUserController";
import { GetUserController } from "../controllers/users/GetUserController";  
import { AuthUserController } from "../controllers/users/AuthUserController"; 
import { AuthMiddleware } from "../middleware/auth";
import { CreateApplicationController } from "../controllers/applications/CreateApplicationController";
import { GetApplicationsController } from "../controllers/applications/GetApplicationsController";
import { CreateVacancyController } from "../controllers/vacancies/CreateVacancyController"; 
import { GetVacancyController } from "../controllers/vacancies/GetVacancyControlller"; 
import { GetVacancyInfoController } from "../controllers/vacancies/GetVacancyInfoController"; 
import { DeleteVacancyController } from "../controllers/vacancies/DeleteVacancyController"; 
import { GetStatisticsController } from "../controllers/statistics/getStatisticsController";
import { GetAllApplicationsController } from "../controllers/applications/GetAllAplicationsController";
import { UpdateVacancyController } from "../controllers/vacancies/UpdateVacancyController";

const createUserController = new CreateUserController();
const getUserController = new GetUserController();
const authUserController = new AuthUserController();
const createApplicationController = new CreateApplicationController();
const getApplicationsController = new GetApplicationsController();
const createVacancyController = new CreateVacancyController();
const getVacancyController = new GetVacancyController();
const getVacancyInfoController = new GetVacancyInfoController();
const deleteVacancyController = new DeleteVacancyController();
const getStatisticsController = new GetStatisticsController();
const getAllApplicationsController = new GetAllApplicationsController();
const updateVacancyController = new UpdateVacancyController();

export const router = Router();

router.post("/users", (req, res) => {createUserController.create(req, res)});

router.get("/users", AuthMiddleware, (req, res) => {getUserController.showUsers(req, res)});

router.post("/users/auth", (req, res) => {authUserController.authenticate(req, res)});

router.get("/applications/", (req,res)=>{getAllApplicationsController.get(req,res)})

router.post("/applications/:vacancyID", (req,res)=>{createApplicationController.apply(req,res)});

router.get("/applications/:vacancyID", (req,res)=>{getApplicationsController.get(req,res)});

router.post("/vacancies", (req,res)=>{createVacancyController.create(req,res)});

router.get("/vacancies", (req,res)=>{getVacancyController.get(req,res)});

router.get("/vacancies/:vacancyID", (req,res)=>{getVacancyInfoController.get(req,res)});

router.delete("/vacancies/:vacancyID", (req,res)=>{deleteVacancyController.delete(req,res)});

router.put("/vacancies/:vacancyID", (req,res)=>{updateVacancyController.update(req,res)});

router.get("/statistics", (req,res)=>{getStatisticsController.get(req,res)})
