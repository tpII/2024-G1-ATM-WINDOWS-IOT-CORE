import express from "express";
import { 
    getCount,
    getTransactions, 
    createTransaction,
    deleteTransaction
} from "../controllers/transaction.controller.js";

const router = express.Router();

router.get("/count", getCount);

router.get("/", getTransactions);

router.post("/", createTransaction);

router.delete("/:id", deleteTransaction);

export default router;