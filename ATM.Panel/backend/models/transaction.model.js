import mongoose from 'mongoose';

const { Schema, model } = mongoose;

const transactionSchema = new Schema({
    type: { 
        type: String, 
        enum: ['deposit', 'withdraw', 'transfer'], 
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
    cardId: { 
        type: Schema.Types.ObjectId, 
        ref: 'Card',
    },
    destinationAccountId: { 
        type: Schema.Types.ObjectId, 
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
