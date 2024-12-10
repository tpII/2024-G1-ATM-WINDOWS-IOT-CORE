import mongoose from 'mongoose';

const { Schema, model } = mongoose;

const transactionSchema = new Schema({
    type: { 
        type: String, 
        enum: ['Deposit', 'Withdraw', 'Transfer'], 
        required: true 
    },
    status: { 
        type: String, 
        enum: ['pending', 'completed', 'failed'], 
        required: true,
        default: 'pending'
    },
    amount: { 
        type: Number, 
        required: true 
    },
    accountId: { 
        type: Schema.Types.ObjectId, 
        ref: 'Account', 
        required: true 
    },
    destinationCbu: { 
        type: String, 
        ref: 'Account',
    },
    createdAt: { 
        type: Date, 
        default: Date.now 
    },
    description: { 
        type: String 
    }
});

const Transaction = model('Transaction', transactionSchema);

export default Transaction;
