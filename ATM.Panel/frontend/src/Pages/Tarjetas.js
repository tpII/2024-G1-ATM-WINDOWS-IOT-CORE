import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import './Tarjetas.css'; // Asegúrate de tener un archivo de estilos

function Tarjetas() {
  const [tarjetas, setTarjetas] = useState([]);
  const [loading, setLoading] = useState(true);
  const [totalTarjetas, setTotalTarjetas] = useState(0);

  // Simulando la carga de datos de tarjetas
  useEffect(() => {
    fetchTarjetas();
  }, []);

  const fetchTarjetas = async () => {
    // Simulando tarjetas por ahora
    const tarjetasData = [
      {
        id: 1,
        number: '1234567812345678',
        pin: '****', // Ocultamos el PIN
        clientId: 'Cliente 1',
        accountId: 'Cuenta 1',
        isActive: true,
        expirationDate: '12/24',
        createdAt: '2023-01-01',
        updatedAt: '2023-06-01',
      },
      {
        id: 2,
        number: '9876543298765432',
        pin: '****',
        clientId: 'Cliente 2',
        accountId: 'Cuenta 2',
        isActive: false,
        expirationDate: '11/23',
        createdAt: '2023-03-15',
        updatedAt: '2023-08-20',
      },
    ];
    setTarjetas(tarjetasData);
    setTotalTarjetas(tarjetasData.length); // Total de tarjetas
    setLoading(false);
  };

  const eliminarTarjeta = (id) => {
    if (window.confirm('¿Estás seguro de eliminar esta tarjeta?')) {
      setTarjetas(tarjetas.filter((tarjeta) => tarjeta.id !== id));
      // Aquí iría la lógica para eliminar la tarjeta desde la API
    }
  };

  return (
    <div className="tarjetas-container">
      <h1>Tarjetas</h1>
      <p>Total de tarjetas: {totalTarjetas}</p>
      <div>
        <Link to="/tarjetas/agregar">
          <button className="btn agregar-btn">Agregar Tarjeta</button>
        </Link>
      </div>
      {loading ? (
        <p>Cargando tarjetas...</p>
      ) : (
        <table className="tarjetas-table">
          <thead>
            <tr>
              <th>Número</th>
              <th>Cliente</th>
              <th>Cuenta</th>
              <th>Estado</th>
              <th>Fecha de Expiración</th>
              <th>Creada</th>
              <th>Actualizada</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {tarjetas.map((tarjeta) => (
              <tr key={tarjeta.id}>
                <td>{tarjeta.number}</td>
                <td>{tarjeta.clientId}</td>
                <td>{tarjeta.accountId}</td>
                <td>{tarjeta.isActive ? 'Activa' : 'Inactiva'}</td>
                <td>{tarjeta.expirationDate}</td>
                <td>{new Date(tarjeta.createdAt).toLocaleDateString()}</td>
                <td>{new Date(tarjeta.updatedAt).toLocaleDateString()}</td>
                <td>
                  <button
                    className="btn eliminar-btn"
                    onClick={() => eliminarTarjeta(tarjeta.id)}
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

export default Tarjetas;
