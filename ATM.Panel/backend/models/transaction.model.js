import mongoose from 'mongoose';

const { Schema, model } = mongoose;

const transactionSchema = new Schema({
    transactionType: { 
        type: String, 
        enum: ['deposit', 'withdrawal', 'transfer'], 
        required: true 
    },
    transactionStatus: { 
        type: String, 
        enum: ['pending', 'completed', 'failed'], 
        required: true 
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
    performedBy: { 
        type: Schema.Types.ObjectId, 
        ref: 'Client',
    },
    description: { 
        type: String 
    }
});

const Transaction = model('Transaction', transactionSchema);

export default Transaction;
