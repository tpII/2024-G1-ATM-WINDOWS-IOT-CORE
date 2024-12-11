import React, { useState, useEffect } from 'react';
import styles from './LimitesExtraccion.module.css'; // Importa los estilos como módulo

function Limites() {
  const [limits, setLimits] = useState({ max: 0, min: 0 });
  const [loading, setLoading] = useState(true);
  const [newLimits, setNewLimits] = useState({ max: '', min: '' });
  const [message, setMessage] = useState('');

  // Cargar los límites al montar el componente
  useEffect(() => {
    fetchLimits();
  }, []);

  // Obtener límites actuales
  const fetchLimits = async () => {
    setLoading(true);
    try {
      const response = await fetch('http://192.168.0.138:5000/api/config/get-limits');
      const data = await response.json();
      setLimits({
        max: data.maxLimit,
        min: data.minLimit,
      });
      setLoading(false);
    } catch (error) {
      console.error('Error al obtener los límites:', error);
      setLoading(false);
    }
  };

  // Actualizar límites
  const updateLimits = async () => {
    const maxLimit = parseInt(newLimits.max, 10);
    const minLimit = parseInt(newLimits.min, 10);

    if (isNaN(maxLimit) || isNaN(minLimit)) {
      setMessage('Por favor, ingrese valores válidos para los límites.');
      return;
    }

    try {
      const response = await fetch('http://192.168.0.138:5000/api/config/set-limits', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ maxLimit, minLimit }),
      });

      if (response.ok) {
        setMessage('Límites actualizados correctamente.');
        fetchLimits(); // Refrescar los límites
      } else {
        const result = await response.json();
        setMessage(`Error al actualizar límites: ${result.message || 'Desconocido'}`);
      }
    } catch (error) {
      console.error('Error al actualizar los límites:', error);
      setMessage('Hubo un problema al conectar con el servidor.');
    }
  };

  return (
    <div className={styles.container}> {/* Aplica los estilos del módulo */}
      <h1 className={styles.title}>Gestión de Límites</h1>
      {loading ? (
        <p className={styles.loading}>Cargando límites...</p>
      ) : (
        <div>
          <p className={styles.info}>Límite Máximo: {limits.max}</p>
          <p className={styles.info}>Límite Mínimo: {limits.min}</p>
        </div>
      )}
      <h2 className={styles.subtitle}>Actualizar Límites</h2>
      <div>
        <input
          type="number"
          placeholder="Nuevo Límite Máximo"
          value={newLimits.max}
          onChange={(e) => setNewLimits({ ...newLimits, max: e.target.value })}
          className={styles.input}
        />
        <input
          type="number"
          placeholder="Nuevo Límite Mínimo"
          value={newLimits.min}
          onChange={(e) => setNewLimits({ ...newLimits, min: e.target.value })}
          className={styles.input}
        />
      </div>
      <button onClick={updateLimits} className={styles.button}>Establecer Límites</button>
      {message && (
        <p className={message.includes('actualizados') ? styles.success : styles.message}>
          {message}
        </p>
      )}
    </div>
  );
}

export default Limites;
