import React, { useState, useEffect } from 'react';
import './Transacciones.css';

function Transacciones() {
  const [transacciones, setTransacciones] = useState([]);
  const [loading, setLoading] = useState(true);
  const [totalTransacciones, setTotalTransacciones] = useState(0);

  // Simulando la carga de datos de transacciones
  useEffect(() => {
    fetchTransacciones();
  }, []);

  const fetchTransacciones = async () => {
    try {
      const response = await fetch('http://localhost:5000/api/transactions');
      const data = await response.json();
      setTransacciones(data.data);  // Asumiendo que 'data' es la clave que contiene la lista de clientes
      setTotalTransacciones(data.data.length); 
      setLoading(false);
    } catch (error) {
      console.log('Error al cargar las transacciones:', error);
      setLoading(false);
    }
  };

  const eliminarTransaccion = async (id) => {
    if (window.confirm('¿Estás seguro de eliminar a esta transacción?')) {
      try {
        const response = await fetch(`http://localhost:5000/api/transactions/${id}`, {
          method: 'DELETE',
        });
  
        const result = await response.json();
        if (result.success) {
          setTransacciones(transacciones.filter(transaccion => transaccion._id !== id));
          console.log('Transacción eliminada');
        } else {
          console.error('Error al eliminar transacción:', result.message);
        }
      } catch (error) {
        console.error('Error al eliminar transacción:', error);
      }
    }
  };
  

  return (
    <div className="transacciones-container">
      <h1>Transacciones</h1>
      <p>Total de transacciones: {totalTransacciones}</p>
      {loading ? (
        <p>Cargando transacciones...</p>
      ) : (
        <div className="transacciones-table-wrapper">
          <table className="transacciones-table">
            <thead>
              <tr>
                <th>ID</th>
                <th>Tipo</th>
                <th>Estado</th>
                <th>Monto</th>
                <th>ID de Cuenta</th>
                <th>ID de Tarjeta</th>
                <th>ID Cuenta Destino</th>
                <th>Fecha</th>
                <th>Realizado por</th>
                <th>Descripción</th>
                <th>Acciones</th>
              </tr>
            </thead>
            <tbody>
              {transacciones.map(transaccion => (
                <tr key={transaccion._id}>
                  <td>{transaccion._id}</td>
                  <td>{transaccion.type}</td>
                  <td>{transaccion.status}</td>
                  <td>${transaccion.amount}</td>
                  <td>{transaccion.accountId}</td>
                  <td>{transaccion.cardId || 'N/A'}</td>
                  <td>{transaccion.destinationAccountId || 'N/A'}</td>
                  <td>{new Date(transaccion.createdAt).toLocaleString()}</td>
                  <td>{transaccion.performedBy || 'N/A'}</td>
                  <td>{transaccion.description || 'N/A'}</td>
                  <td>
                    <button
                      className="btn eliminar-btn"
                      onClick={() => eliminarTransaccion(transaccion._id)}
                    >
                      Eliminar
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>

  );
}

export default Transacciones;
