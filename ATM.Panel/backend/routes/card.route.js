import express from "express";
import { 
    getCount,
    getCards, 
    createCard, 
    deleteCard, 
    verifyPin,
    exists,
    deactivateCard,
    activateCard
} from "../controllers/card.controller.js";

const router = express.Router();

router.get("/count", getCount);

router.get("/", getCards);

router.post("/", createCard);

router.delete("/:id", deleteCard);

router.post("/verify-pin", verifyPin);

router.get("/exists/:nro", exists);

router.patch("/ban/:nro", deactivateCard);

router.patch("/unban/:nro", activateCard);

export default router;