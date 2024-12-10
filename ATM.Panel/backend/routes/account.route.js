import express from "express";
import { 
    getCount,
    getAccounts, 
    getBalance,
    createAccount, 
    deleteAccount
} from "../controllers/account.controller.js";

const router = express.Router();

router.get("/count", getCount);

router.get("/", getAccounts);

router.get("/balance/:id", getBalance);

router.post("/", createAccount);

router.delete("/:id", deleteAccount);

export default router;