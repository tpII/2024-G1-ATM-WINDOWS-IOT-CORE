import mongoose from 'mongoose';

const { Schema, model } = mongoose;

const accountSchema = new Schema({
    number: { 
        type: String, 
        required: true, 
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

accountSchema.index({ number: 1, cbu: 1 }, { unique: true });

const Account = model('Account', accountSchema);

export default Account;
  