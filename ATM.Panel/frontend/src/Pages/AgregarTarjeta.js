import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './AgregarTarjeta.css';

function AgregarTarjeta() {
  const [number, setNumber] = useState('');
  const [pin, setPin] = useState('');
  const [expirationDate, setExpirationDate] = useState('');
  const [clientId, setClientId] = useState('');
  const [accountId, setAccountId] = useState('');
  const [pinError, setPinError] = useState('');
  const navigate = useNavigate();


  const handleSubmit = async (e) => {
    e.preventDefault();

    // Validar PIN (debe ser un número de 4 dígitos)
    const pinRegex = /^\d{4}$/;
    if (!pinRegex.test(pin)) {
      setPinError('El PIN debe ser un número de 4 dígitos.');
      return;
    }
    
    setPinError(''); // Limpiar error si el PIN es válido
  
    const tarjeta = { number: number, pin: pin, clientId: clientId, accountId: accountId, expirationDate: expirationDate };
  
    try {
      const response = await fetch('http://localhost:5000/api/cards', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(tarjeta),
      });
  
      const result = await response.json();
      if (result.success) {
        console.log('Tarjeta agregada:', result.data);
        navigate('/tarjetas'); // Redirige después de agregar
      } else {
        console.log('Error al agregar tarjeta:', result.message);
      }
    } catch (error) {
      console.error('Error en el servidor:', error);
    }
  };

  return (
    <div className="agregar-tarjeta-container">
      <h1>Agregar Tarjeta</h1>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Número de Tarjeta"
          value={number}
          onChange={(e) => setNumber(e.target.value)}
          required
        />
        <input
          type="text"
          placeholder="PIN (4 dígitos)"
          value={pin}
          onChange={(e) => setPin(e.target.value)}
          required
        />
        {pinError && <p className="error">{pinError}</p>}
        <input
          type="text"
          placeholder="Fecha de Expiración (MM/YY)"
          value={expirationDate}
          onChange={(e) => setExpirationDate(e.target.value)}
          required
        />
        <input
          type="text"
          placeholder="ID Cliente"
          value={clientId}
          onChange={(e) => setClientId(e.target.value)}
          required
        />
        <input
          type="text"
          placeholder="ID Cuenta"
          value={accountId}
          onChange={(e) => setAccountId(e.target.value)}
          required
        />
        <button type="submit" className="btn agregar-btn">Agregar</button>
      </form>
    </div>
  );
}

export default AgregarTarjeta;
