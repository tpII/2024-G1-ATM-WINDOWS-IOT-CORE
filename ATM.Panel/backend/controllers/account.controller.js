import Account from "../models/account.model.js";
import Client from "../models/client.model.js";
import mongoose from "mongoose";

export const getCount = async(req, res) => {
    const c = await Account.countDocuments({});
    console.log(`Contando cuentas: ${c}`);
    res.json({count: c})
};

export const getAccounts = async (req,res) => {    
    try {
        const accounts = await Account.find({});
        res.status(200).json({ 
            success: true, 
            data: accounts 
        });
    } catch (error) {
        console.log("Ocurrió un error al recuperar las cuentas: ", error.message);
        res.status(404).json({ 
            success: false, 
            message: "Ocurrió un error al obtener las cuentas",
            error: error.message
        })
    }
};

export const createAccount = async (req, res) => {
    const account = req.body;

    // Validar ID del cliente
    if (!mongoose.Types.ObjectId.isValid(account.clientId)) {
        return res.status(404).json({ 
            success: false, 
            message: "ID del cliente inválido" 
        });
    }

    // Validar campos obligatorios
    if (!account.number || !account.cbu || !account.clientId) {
        return res.status(400).json({ 
            success: false, 
            message: "Por favor provea todos los campos" 
        });
    }

    try {
        // Comprobar si el cliente existe
        const client = await Client.findById(account.clientId);
        if (!client) {
            return res.status(400).json({ 
                success: false, 
                message: `Cliente no encontrado` 
            });
        }

        // Crear nueva cuenta
        const newAccount = new Account(account);

        await newAccount.save();
        res.status(201).json({ success: true, data: newAccount });
    } catch (error) {
        console.error(`Error al crear una cuenta: ${error.message}`);
        if (error.code === 11000) {
            const duplicateKey = Object.keys(error.keyPattern)[0];
            return res.status(400).json({
                success: false,
                message: `El campo ${duplicateKey} ya existe.`
            });
        }
        res.status(500).json({
            success: false,
            message: "Error del servidor",
            error: error.message
        });
    }
};

export const deleteAccount = async (req,res) => {
    const {id} = req.params;
    
    if(!mongoose.Types.ObjectId.isValid(id))
    {
        return res.status(404).json({ 
            success: false, 
            message: "ID inválido" 
        });
    }

    try {
        const deletedAccount = await Account.findByIdAndDelete(id);
        if (!deletedAccount) {
            return res.status(404).json({ 
                success: false, 
                message: "Cuenta no encontrada" 
            });
        }

        res.status(200).json({ success: true, message: "Cuenta borrada" });
    } catch (error) {
        console.log("Error al borrar una cuenta: ", error.message);
        res.status(500).json({ 
            success: false, 
            message: "Error del servidor",
            error: error.message
        })
    }
};
