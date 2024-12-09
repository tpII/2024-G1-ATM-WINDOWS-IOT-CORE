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
        required: true
    },
    clientId: { 
        type: Schema.Types.ObjectId, 
        ref: 'Client', 
        required: true 
    },
    balance: { 
        type: Number, 
        required: true 
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
