import React, { useState } from 'react';

function LimitesExtraccion() {
  const [limits, setLimits] = useState({ max: '', min: '' });
  const [newLimits, setNewLimits] = useState({ max: '', min: '' });
  const [message, setMessage] = useState('');

  // Función para obtener los límites actuales
  const fetchLimits = async () => {
    try {
      const response = await fetch('http://192.168.0.138:5000/api/config/get-limits', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        const data = await response.json();
        setLimits({
          max: data.maxLimit,
          min: data.minLimit,
        });
      } else {
        console.error('Error al obtener los límites:', response.statusText);
      }
    } catch (error) {
      console.error('Error de red:', error);
    }
  };

  // Función para actualizar los límites
  const updateLimits = async () => {
    try {
      const response = await fetch('http://192.168.0.138:5000/api/config/set-limits', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          maxLimit: parseInt(newLimits.max),
          minLimit: parseInt(newLimits.min),
        }),
      });

      if (response.ok) {
        setMessage('Límites actualizados correctamente.');
        fetchLimits(); // Refrescar los límites
      } else {
        console.error('Error al actualizar los límites:', response.statusText);
      }
    } catch (error) {
      console.error('Error de red:', error);
    }
  };

  return (
    <div>
      <h1>Límites de Extracción</h1>
      <button onClick={fetchLimits}>Consultar Límites</button>
      {limits.max && limits.min && (
        <div>
          <p>Límite Máximo: {limits.max}</p>
          <p>Límite Mínimo: {limits.min}</p>
        </div>
      )}
      <h2>Actualizar Límites</h2>
      <input
        type="number"
        placeholder="Nuevo Límite Máximo"
        value={newLimits.max}
        onChange={(e) => setNewLimits({ ...newLimits, max: e.target.value })}
      />
      <input
        type="number"
        placeholder="Nuevo Límite Mínimo"
        value={newLimits.min}
        onChange={(e) => setNewLimits({ ...newLimits, min: e.target.value })}
      />
      <button onClick={updateLimits}>Establecer Límites</button>
      {message && <p>{message}</p>}
    </div>
  );
}

export default LimitesExtraccion;
