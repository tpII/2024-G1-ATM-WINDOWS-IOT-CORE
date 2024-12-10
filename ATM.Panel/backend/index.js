import express from 'express';
import dotenv from 'dotenv';
import { connectDB } from './config/db.js';

import accountRoutes from "./routes/account.route.js"
// import cardRoutes from "./routes/card.route.js"
import clientRoutes from "./routes/client.route.js"
// import transactionRoutes from "./routes/transaction.route.js"

dotenv.config();
const PORT = process.env.PORT || 5000;
const IP = `${process.env.IP}` || 'locahost';

const app = express();

app.use(express.json()); //allows us to accept JSON data in the body

app.use("/api/accounts", accountRoutes);
// app.use("/api/cards", cardRoutes);
app.use("/api/clients", clientRoutes);
// app.use("/api/transactions", transactionRoutes);

app.listen(PORT, IP, () => {
    connectDB();
    console.log("Server started at https://" + IP + ":" + PORT);
});