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
    try {
      const response = await fetch('http://localhost:5000/api/cards');
      const data = await response.json();
      setTarjetas(data.data);  // Asumiendo que 'data' es la clave que contiene la lista de clientes
      setLoading(false);
    } catch (error) {
      console.log('Error al cargar las tarjetas:', error);
      setLoading(false);
    }
  };

  const eliminarTarjeta = async (id) => {
    if (window.confirm('¿Estás seguro de eliminar esta tarjeta?')) {
      try {
        const response = await fetch(`http://localhost:5000/api/cards/${id}`, {
          method: 'DELETE',
        });
  
        const result = await response.json();
        if (result.success) {
          setTarjetas(tarjetas.filter(tarjeta => tarjeta._id !== id));
          console.log('Tarjeta eliminada');
        } else {
          console.error('Error al eliminar tarjeta:', result.message);
        }
      } catch (error) {
        console.error('Error al eliminar tarjeta:', error);
      }
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
                    onClick={() => eliminarTarjeta(tarjeta._id)}
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
