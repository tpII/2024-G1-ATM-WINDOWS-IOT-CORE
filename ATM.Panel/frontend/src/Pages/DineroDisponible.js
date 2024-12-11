import React, { useState } from 'react';

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

  return (
    <div>
      <h1>Dinero Disponible</h1>
      <button onClick={fetchAvailableCash}>Consultar Dinero Disponible</button>
      {availableCash && <p>Dinero Disponible: {availableCash}</p>}
      <h2>Recargar Dinero</h2>
      <input
        type="number"
        placeholder="Cantidad a recargar"
        value={reloadAmount}
        onChange={(e) => setReloadAmount(e.target.value)}
      />
      <button onClick={reloadCash}>Recargar</button>
      {message && <p>{message}</p>}
    </div>
  );
}

export default DineroDisponible;
