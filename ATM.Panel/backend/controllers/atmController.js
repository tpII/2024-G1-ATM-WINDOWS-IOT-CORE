const axios = require('axios');

const RASPBERRY_PI_URL = 'http://192.168.0.138/api';

const getWithdrawalLimits = async (req, res) => {
    try {
        const response = await axios.get(`${RASPBERRY_PI_URL}/config/get-limits`);
        res.json(response.data);
    } catch (error) {
        console.error('Error fetching withdrawal limits:', error.message);
        res.status(500).json({ error: 'Error fetching withdrawal limits' });
    }
};

const getAvailableCash = async (req, res) => {
    try {
        const response = await axios.get(`${RASPBERRY_PI_URL}/cash-management/available-cash`);
        res.json(response.data);
    } catch (error) {
        console.error('Error fetching available cash:', error.message);
        res.status(500).json({ error: 'Error fetching available cash' });
    }
};

module.exports = { getWithdrawalLimits, getAvailableCash };
