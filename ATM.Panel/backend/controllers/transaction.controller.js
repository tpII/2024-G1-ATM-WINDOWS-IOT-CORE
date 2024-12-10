import Transaction from "../models/transaction.model.js";
import Account from "../models/account.model.js";
import mongoose from "mongoose";


export const getCount = async(req, res) => {
    const c = await Transaction.countDocuments({});
    console.log(`Contando transacciones: ${c}`);
    res.json({count: c})
};


export const getTransactions = async (req, res) => {
    try {
        const transactions = await Transaction.find({}); 

        res.status(200).json({
            success: true,
            data: transactions,
        });
    } catch (error) {
        console.error("Error al obtener transacciones: ", error.message);
        res.status(500).json({
            success: false,
            message: "Error al obtener transacciones",
            error: error.message,
        });
    }
};


export const createTransaction = async (req, res) => {
    const transaction = req.body;

    const validTransactionTypes = ["deposit", "withdrawal", "transfer"];
    if (!validTransactionTypes.includes(transaction.transactionType)) {
        return res.status(400).json({
            success: false,
            message: "Tipo de transacción inválido"
        });
    }

    if (transaction.amount <= 0) {
        return res.status(400).json({
            success: false,
            message: "El monto debe ser mayor a 0"
        });
    }

    try {
        const sourceAccount = await Account.findById(transaction.accountId);
        if (!sourceAccount) {
            return res.status(404).json({
                success: false,
                message: "Cuenta origen no encontrada"
            });
        }

        if (transaction.transactionType === "withdrawal" || transaction.transactionType === "transfer") {
            if (sourceAccount.balance < transaction.amount) {
                return res.status(400).json({
                    success: false,
                    message: "Saldo insuficiente"
                });
            }
        }

        let destinationAccount;
        if (transaction.transactionType === "transfer") {
            destinationAccount = await Account.findById(transaction.destinationAccountId);
            if (!destinationAccount) {
                return res.status(404).json({
                    success: false,
                    message: "Cuenta destino no encontrada"
                });
            }
        }

        // Crear la transacción
        const newTransaction = new Transaction(transaction);

        if (transaction.transactionType === "deposit") {
            sourceAccount.balance += transaction.amount;
        } else if (transaction.transactionType === "withdrawal") {
            sourceAccount.balance -= transaction.amount;
        } else if (transaction.transactionType === "transfer") {
            sourceAccount.balance -= transaction.amount;
            destinationAccount.balance += transaction.amount;
        }

        // Guardar la transacción y las cuentas actualizadas
        await sourceAccount.save();
        if (destinationAccount) {
            await destinationAccount.save();
        }
        await newTransaction.save();

        res.status(201).json({
            success: true,
            data: newTransaction
        });
    } catch (error) {
        console.error("Error al crear transacción: ", error.message);
        res.status(500).json({
            success: false,
            message: "Error del servidor",
            error: error.message
        });
    }
};

export const deleteTransaction = async (req, res) => {
    const { id } = req.params;

    if (!mongoose.Types.ObjectId.isValid(id)) {
        return res.status(404).json({
            success: false,
            message: "ID de transacción inválido"
        });
    }

    try {
        const transaction = await Transaction.findByIdAndDelete(id);

        if (!transaction) {
            return res.status(404).json({
                success: false,
                message: "Transacción no encontrada"
            });
        }

        res.status(200).json({
            success: true,
            message: "Transacción eliminada",
            data: transaction
        });
    } catch (error) {
        console.log("Error al borrar una transacción: ", error.message);
        res.status(500).json({
            success: false,
            message: "Error del servidor",
            error: error.message
        });
    }
};
