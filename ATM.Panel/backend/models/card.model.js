import mongoose from 'mongoose';

const { Schema, model } = mongoose;

const cardSchema = new Schema({
    number: { 
        type: String, 
        required: true
    },
    pin: { 
        type: String, 
        required: true 
    },
    clientId: { 
        type: Schema.Types.ObjectId, 
        ref: 'Client', 
        required: true 
    },
    accountId: { 
        type: Schema.Types.ObjectId, 
        ref: 'Account', 
        required: true 
    },
    isActive: { 
        type: Boolean, 
        default: true 
    },
    expirationDate: { 
        type: String, 
        required: true,
        match: /^(0[1-9]|1[0-2])\/\d{2}$/ // Validar formato MM/YY
    }
}, {
    timestamps: true // Agrega createdAt y updatedAt automáticamente
});

const Card = model('Card', cardSchema);

export default Card;
