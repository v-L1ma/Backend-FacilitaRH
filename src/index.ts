import express from "express";
import { router } from "./Routes/Routes";
import cors from "cors";

const app = express();

app.use(cors())
app.use(express.json());
app.use(router);


app.listen(3000, ()=>console.log("Server on"));