import express from 'express';
import dotenv from 'dotenv';
import { connectDB } from './config/db.js';

import productRoutes from "./routes/product.route.js"
import depositRoutes from "./routes/deposit.route.js"

dotenv.config();
const PORT = process.env.PORT || 5000;

const app = express();

app.use(express.json()); //allows us to accept JSON data in the body

app.use("/api/products", productRoutes);
app.use("/api/deposit", depositRoutes);

app.listen(PORT, () => {
    connectDB();
    console.log("Server started at https://localhost:" + PORT);
});