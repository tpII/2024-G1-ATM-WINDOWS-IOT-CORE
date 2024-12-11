const express = require('express');
const { getWithdrawalLimits, getAvailableCash } = require('../controllers/atmController');

const router = express.Router();

router.get('/limits', getWithdrawalLimits);
router.get('/cash', getAvailableCash);

module.exports = router;
