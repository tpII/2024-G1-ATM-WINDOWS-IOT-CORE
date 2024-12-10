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
    // Aquí iría la lógica para obtener las transacciones desde el backend
    // Simulando datos:
    const transaccionesData = [
      {
        id: 1,
        transactionType: 'deposit',
        transactionStatus: 'completed',
        amount: 500,
        accountId: 'A12345',
        cardId: 'C98765',
        destinationAccountId: null,
        createdAt: '2024-12-01T10:30:00Z',
        performedBy: 'Client01',
        description: 'Depósito inicial',
      },
      {
        id: 2,
        transactionType: 'withdrawal',
        transactionStatus: 'pending',
        amount: 300,
        accountId: 'A67890',
        cardId: null,
        destinationAccountId: null,
        createdAt: '2024-12-02T15:45:00Z',
        performedBy: 'Client02',
        description: 'Retiro en efectivo',
      },
    ];
    setTransacciones(transaccionesData);
    setTotalTransacciones(transaccionesData.length);
    setLoading(false);
  };

  const eliminarTransaccion = (id) => {
    if (window.confirm('¿Estás seguro de eliminar esta transacción?')) {
      setTransacciones(transacciones.filter(transaccion => transaccion.id !== id));
      // Aquí iría la lógica para eliminar la transacción desde la API
    }
  };

  return (
    <div className="transacciones-container">
      <h1>Transacciones</h1>
      <p>Total de transacciones: {totalTransacciones}</p>
      {loading ? (
        <p>Cargando transacciones...</p>
      ) : (
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
              <tr key={transaccion.id}>
                <td>{transaccion.id}</td>
                <td>{transaccion.transactionType}</td>
                <td>{transaccion.transactionStatus}</td>
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
                    onClick={() => eliminarTransaccion(transaccion.id)}
                  >
                    Eliminar
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default Transacciones;
