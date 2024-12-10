import Card from "../models/card.model.js";
import Client from "../models/client.model.js";
import Account from "../models/account.model.js";
import mongoose from "mongoose";
import crypto from "crypto";

function createHash(password) {
    return crypto.createHash('sha256').update(password).digest('base64');
}

export const getCount = async(req, res) => {
    const c = await Card.countDocuments({});
    res.json({count: c})
};

export const getCards = async (req,res) => {
    try {
        const cards = await Card.find({});
        res.status(200).json({ success: true, data: cards });
    } catch (error) {
        console.log("Ocurrio un error al obtener las tarjetas: ", error.message);
        res.status(404).json({ 
            success: false, 
            message: "Ocurrio un error al obtener las tarjetas",
            error: error.message
        })
    }
};

export const createCard = async (req,res) => {
    let card = req.body;

    if (!card.number || !card.pin || !card.clientId || !card.accountId || !card.expirationDate ) {
        return res.status(400).json({ 
            success: false, 
            message: "Por favor provea todos los campos" 
        })
    }

    try {
        // Comprobar si el cliente existe
        const client = await Client.findById(card.clientId);
        if (!client) {
            return res.status(400).json({ 
                success: false, 
                message: `Cliente no encontrado` 
            });
        }

        // Comprobar si el cliente existe
        const account = await Account.findById(card.accountId);
        if (!account) {
            return res.status(400).json({ 
                success: false, 
                message: `Cuenta no encontrada` 
            });
        }

        card.pin = createHash(card.pin);

        const newCard = new Card(card);
        await newCard.save();
        res.status(201).json({ success: true, data: newCard });
    } catch (error) {
        console.log(`Error al crear una tarjeta: ${error.message}`);
        if (error.code === 11000) { // Código de error para duplicados en MongoDB
            return res.status(400).json({
                success: false, 
                message: "Numero de tarjeta ya existe" 
            });
        }
        res.status(500).json({ 
            success: false, 
            message: "Error del servidor",
            error: error.message
        });
    }
};

export const deleteCard = async (req,res) => {
    const {id} = req.params;
    
    if(!mongoose.Types.ObjectId.isValid(id))
    {
        return res.status(404).json({
            success: false, 
            message: "ID inválido" 
        });
    }

    try {
        const deletedCard = await Card.findByIdAndDelete(id);
        if (!deletedCard) {
            return res.status(404).json({ 
                success: false, 
                message: "Tarjeta no encontrada" 
            });
        }

        res.status(200).json({ success: true, message: "Tarjeta borrada" });
    } catch (error) {
        console.log("Error al borrar una tarjeta: ", error.message);
        res.status(500).json({ 
            success: false, 
            message: "Error del servidor",
            error: error.message
        })
    }
};

export const exists = async (req,res) => {
    const {nro} = req.params;
    
    try {
        const card = await Card.findOne({ number : nro });
        if (!card) {
            return res.status(404).json({ 
                success: false, 
                message: "Tarjeta no encontrada" 
            });
        }

        res.status(200).json({ success: true, message: "Tarjeta registrada" });
    } catch (error) {
        console.log("Error al buscar una tarjeta: ", error.message);
        res.status(500).json({ 
            success: false, 
            message: "Error del servidor",
            error: error.message
        })
    }
};

export const verifyPin = async (req,res) => {
    const { nro, pin } = req.body;

    try {
        const card = await Card.findOne({ number : nro });
        if (!card || card.pin != pin) {
            return res.status(404).json({ 
                success: false, 
                message: "La tarjeta o el pin son inválidos" 
            });
        }

        res.status(200).json({ success: true, message: "Pin verificado" });
    } catch (error) {
        console.log("Error al verificar un pin: ", error.message);
        res.status(500).json({ 
            success: false, 
            message: "Error del servidor",
            error: error.message
        })
    }
};

export const deactivateCard = async (req, res) => {
    const { nro } = req.params;
  
    try {
      const updatedCard = await Card.findOneAndUpdate(
        { number : nro },         // Buscar tarjeta por el número de tarjeta
        { isActive: false },    // Actualizar el campo isActive a false
        { new: true }           // Devuelve el documento actualizado
      );
  
      if (!updatedCard) {
        return res.status(404).json({ 
            success: false, 
            message: "Tarjeta no encontrada" 
        });
      }
  
      res.status(200).json({ success: true, data: updatedCard });
    } catch (error) {
        console.error(`Error al desactivar tarjeta: ${error.message}`);
        res.status(500).json({ 
            success: false, 
            message: "Error del servidor" 
        });
    }
};
  

export const activateCard = async (req, res) => {
    const { nro } = req.params;
  
    try {
      const updatedCard = await Card.findOneAndUpdate(
        { number : nro },         // Buscar tarjeta por el número de tarjeta
        { isActive: true },    // Actualizar el campo isActive a false
        { new: true }           // Devuelve el documento actualizado
      );
  
      if (!updatedCard) {
        return res.status(404).json({ 
            success: false, 
            message: "Tarjeta no encontrada" 
        });
      }
  
      res.status(200).json({ success: true, data: updatedCard });
    } catch (error) {
        console.error(`Error al activar tarjeta: ${error.message}`);
        res.status(500).json({ 
            success: false, 
            message: "Error del servidor" 
        });
    }
};