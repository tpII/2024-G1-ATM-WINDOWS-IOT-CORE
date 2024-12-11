import axios from 'axios';

const API_URL = 'http://localhost:5000/api/atm';

export const getWithdrawalLimits = async () => {
    const response = await axios.get(`${API_URL}/limits`);
    return response.data;
};

export const getAvailableCash = async () => {
    const response = await axios.get(`${API_URL}/cash`);
    return response.data;
};
