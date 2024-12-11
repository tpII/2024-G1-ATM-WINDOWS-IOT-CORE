import React, { useState, useEffect } from 'react';
import './DineroDisponible.css'; // Asegúrate de que el archivo CSS esté importado

function DineroDisponible() {
  const [availableCash, setAvailableCash] = useState('');
  const [reloadAmount, setReloadAmount] = useState('');
  const [message, setMessage] = useState('');

  // Función para consultar el dinero disponible
  const fetchAvailableCash = async () => {
    try {
      const response = await fetch('http://192.168.0.138:5000/api/cash-management/available-cash', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        const data = await response.json();
        setAvailableCash(data);
      } else {
        console.error('Error al obtener el dinero disponible:', response.statusText);
      }
    } catch (error) {
      console.error('Error de red:', error);
    }
  };

  // Función para recargar dinero
  const reloadCash = async () => {
    try {
      const response = await fetch('http://192.168.0.138:5000/api/cash-management/load-cash', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(parseInt(reloadAmount)),
      });

      if (response.ok) {
        setMessage('Dinero cargado correctamente.');
        fetchAvailableCash(); // Refrescar el dinero disponible
      } else {
        console.error('Error al recargar dinero:', response.statusText);
      }
    } catch (error) {
      console.error('Error de red:', error);
    }
  };

  // Cargar el dinero disponible al montar el componente
  useEffect(() => {
    fetchAvailableCash();
  }, []);

  return (
    <div className="dinero-container">
      <h1>Cajero ATM</h1>
      {availableCash && <p className="dinero-info">Dinero Disponible: {availableCash}</p>}
      <h2>Recargar Dinero</h2>
      <div>
        <input
          type="number"
          placeholder="Cantidad a recargar"
          value={reloadAmount}
          onChange={(e) => setReloadAmount(e.target.value)}
          className="dinero-input"
        />
      </div>
      <button className="dinero-button" onClick={reloadCash}>Recargar</button>
      {message && <p className="dinero-message">{message}</p>}
    </div>
  );
}

export default DineroDisponible;
