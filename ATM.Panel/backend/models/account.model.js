import mongoose from 'mongoose';

const { Schema, model } = mongoose;

const accountSchema = new Schema({
    accountNumber: { 
        type: String, 
        required: true, 
        unique: true 
    },
    cbu: {
        type: String,
        unique: true,
        required: true
    },
    clientId: { 
        type: Schema.Types.ObjectId, 
        ref: 'Client', 
        required: true 
    },
    balance: { 
        type: Number, 
        required: true,
        default: 0
    }
    // currency: { 
    //     type: String, 
    //     required: true, 
    //     enum: ['USD', 'ARS', 'EUR'] 
    // }
}, {
    timestamps: true
});

const Account = model('Account', accountSchema);

export default Account;
