import mongoose from 'mongoose';

const { Schema, model } = mongoose;

const clientSchema = new Schema({
    fullName: { 
        type: String, 
        required: true 
    },
    email: { 
        type: String, 
        required: true, 
        unique: true 
    },
    phone: { 
        type: String, 
        required: true 
    }
}, {
    timestamps: true
});

const Client = model('Client', clientSchema);

export default Client;
