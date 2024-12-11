import Transaction from "../models/transaction.model.js";
import Account from "../models/account.model.js";
import mongoose from "mongoose";


export const getCount = async(req, res) => {
    const c = await Transaction.countDocuments({});
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

    console.log(req.body);

    const validtypes = ["Deposit", "Withdraw", "Transfer"];
    if (!validtypes.includes(transaction.type)) {
        return res.status(400).json({
            success: false,
            message: "Tipo de transacción inválido"
        });
    }

    if (!mongoose.Types.ObjectId.isValid(transaction.accountId)) {
        return res.status(404).json({ 
            success: false, 
            message: "ID de la cuenta inválido"
        });
    }

    if(transaction.type == "Transfer")
    {
        if(!transaction.destinationCbu)
        {
            return res.status(404).json({ 
                success: false, 
                message: "Falta el ID de la cuenta destino"
            });
        }
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

        if (transaction.type === "Withdraw" | transaction.type === "Transfer") {
            if (sourceAccount.balance < transaction.amount) {
                return res.status(400).json({
                    success: false,
                    message: "Saldo insuficiente"
                });
            }
        }

        let destinationAccount;
        if (transaction.type === "Transfer") {
            destinationAccount = await Account.findOne({ cbu : transaction.destinationCbu });
			
			if (!destinationAccount) {
                return res.status(404).json({
                    success: false,
                    message: "Cuenta destino no encontrada"
                });
            }

            if(transaction.accountId == destinationAccount.id)
            {
                return res.status(404).json({
                    success: false,
                    message: "La cuenta destino debe ser distinta a la cuenta origen. Coloque un CBU que no sea el propio"
                });
            }
        }

        // Crear la transacción
        const newTransaction = new Transaction(transaction);

        if (transaction.type === "Deposit") {
            sourceAccount.balance += transaction.amount;
        } else if (transaction.type === "Withdraw") {
            sourceAccount.balance -= transaction.amount;
        } else if (transaction.type === "Transfer") {
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
