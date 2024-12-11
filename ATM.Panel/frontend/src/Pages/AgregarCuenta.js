import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './AgregarCuenta.css';

function AgregarCuenta() {
  const [number, setNumber] = useState('');
  const [cbu, setCbu] = useState('');
  const [clientId, setClientId] = useState('');
  const navigate = useNavigate();
  

  const handleSubmit = async (e) => {
    e.preventDefault();
  
    const cuenta = { number: number, cbu: cbu, clientId: clientId };
  
    try {
      const response = await fetch('http://localhost:5000/api/accounts', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(cuenta),
      });
  
      const result = await response.json();
      if (result.success) {
        console.log('Cuenta agregada:', result.data);
        navigate('/cuentas'); // Redirige después de agregar
      } else {
        console.log('Error al agregar cuenta:', result.message);
      }
    } catch (error) {
      console.error('Error en el servidor:', error);
    }
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
