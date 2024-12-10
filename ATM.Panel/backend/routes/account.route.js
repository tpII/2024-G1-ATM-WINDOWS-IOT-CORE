import express from "express";
import { 
    getCount,
    getAccounts, 
    createAccount, 
    deleteAccount
} from "../controllers/account.controller.js";

const router = express.Router();

router.get("/count", getCount);

router.get("/", getAccounts);

router.post("/", createAccount);

router.delete("/:id", deleteAccount);

export default router;