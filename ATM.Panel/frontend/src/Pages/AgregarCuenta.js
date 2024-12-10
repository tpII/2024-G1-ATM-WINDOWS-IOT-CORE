import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './AgregarCuenta.css';

function AgregarCuenta() {
  const [number, setNumber] = useState('');
  const [cbu, setCbu] = useState('');
  const [balance, setBalance] = useState(0);
  const [clientId, setClientId] = useState('');
  const navigate = useNavigate();
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    // Aquí iría la lógica para enviar los datos al servidor (API)
    console.log('Cuenta agregada:', { number, cbu, balance, clientId });
    navigate('/cuentas'); // Redirigir a la página de cuentas
  };

  return (
    <div className="agregar-cuenta-container">
      <h1>Agregar Cuenta</h1>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Número de Cuenta"
          value={number}
          onChange={(e) => setNumber(e.target.value)}
          required
        />
        <input
          type="text"
          placeholder="CBU"
          value={cbu}
          onChange={(e) => setCbu(e.target.value)}
          required
        />
        <input
          type="number"
          placeholder="Balance"
          value={balance}
          onChange={(e) => setBalance(e.target.value)}
          required
        />
        <input
          type="text"
          placeholder="ID Cliente"
          value={clientId}
          onChange={(e) => setClientId(e.target.value)}
          required
        />
        <button type="submit" className="btn agregar-btn">Agregar</button>
      </form>
    </div>
  );
}

export default AgregarCuenta;
