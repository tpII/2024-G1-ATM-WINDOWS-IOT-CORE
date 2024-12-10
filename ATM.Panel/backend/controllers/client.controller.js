import Client from "../models/client.model.js";
import mongoose from "mongoose";

export const getCount = async(req, res) => {
    const c = await Client.countDocuments({});
    res.json({count: c})
};

export const getClients = async (req,res) => {
    try {
        // Usando aggregate() en lugar de find(), se puede realizar un "join" entre colecciones
        const clients = await Client.aggregate().lookup({
            from: 'cards',
            localField: '_id',
            foreignField: 'clientId',
            as: 'cards'
        });
        res.status(200).json({ success: true, data: clients });
    } catch (error) {
        console.log("Ocurrió un error al obtener los usuarios: ", error.message);
        res.status(404).json({ success: false, message: 'Ocurrió un error al obtener los usuarios' })
    }
};

export const createClient = async (req,res) => {
    const client = req.body;

    if (!client.fullName || !client.email || !client.phone) {
        return res.status(400).json({ success: false, message: "Por favor provea todos los campos" })
    }

    try {
        const newClient = new Client(client);
        await newClient.save();
        res.status(201).json({ success: true, data: newClient });
    } catch (error) {
        console.log(`Error al crear un cliente: ${error.message}`);
        res.status(500).json({ success: false, message: "Error del servidor" });
    }
};

export const deleteClient = async (req,res) => {
    const {id} = req.params;
    
    if(!mongoose.Types.ObjectId.isValid(id))
    {
        return res.status(404).json({ 
            success: false, 
            message: "ID inválido" 
        });
    }

    try {
        const client = await Client.findByIdAndDelete(id);
        if(!client) {
            return res.status(400).json({ 
                success: false, 
                message: "Cliente no encontrado" 
            });
        }

        res.status(200).json({ success: true, message: "Cliente borrado" });
    } catch (error) {
        console.log("Error al borrar un cliente: ", error.message);
        res.status(500).json({ 
            success: false, 
            message: "Error del servidor" 
        })
    }
};
