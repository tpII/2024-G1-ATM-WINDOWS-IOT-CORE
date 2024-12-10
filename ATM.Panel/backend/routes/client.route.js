import express from "express";
import { 
    getCount,
    getClients,
    createClient, 
    deleteClient
} from "../controllers/client.controller.js";

const router = express.Router();

router.get("/count", getCount);

router.get("/", getClients);

router.post("/", createClient);

router.delete("/:id", deleteClient);

export default router;