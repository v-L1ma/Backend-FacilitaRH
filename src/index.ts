import express from "express";
import { router } from "./Routes/Routes";

const app = express();

app.use(express.json());
app.use(router);


app.listen(3000, ()=>console.log("Server on"));