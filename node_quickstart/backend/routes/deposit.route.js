import express from "express";

const router = express.Router();

async function processDeposit(amount)
{
    console.log(`Depósito recibido: ${amount}`);

    // Simular un pequeño retraso para imitar el procesamiento
    await new Promise(resolve => setTimeout(resolve, 1000));

    // Retornar un mensaje de éxito
    return { success: true, message: 'Depósito procesado correctamente', modifiedAmount: amount * 0.01};
}

router.post('/', async (req, res) => {
    const { amount } = req.body;

    try {
        // Llamar a la función processDeposit
        const result = await processDeposit(amount);
        console.log(`Depósito modificado: ${result.modifiedAmount}`);
        res.status(200).send(result); // Enviar el resultado de processDeposit
    } catch (error) {
        console.error(error);
        res.status(500).send({ success: false, message: 'Error procesando el depósito'});
    }
});

export default router;