import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import './Clientes.css'; // Asegúrate de tener un archivo de estilos

function Clientes() {
  const [clientes, setClientes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [totalClientes, setTotalClientes] = useState(0);

  // Simulando la carga de datos de clientes
  useEffect(() => {
    fetchClientes();
  }, []);

  const fetchClientes = async () => {
    try {
      const response = await fetch('http://localhost:5000/api/clients');
      const data = await response.json();
      setClientes(data.data);  // Asumiendo que 'data' es la clave que contiene la lista de clientes
      setTotalClientes(data.data.length); 
      setLoading(false);
    } catch (error) {
      console.log('Error al cargar los clientes:', error);
      setLoading(false);
    }
  };


  const eliminarCliente = async (id) => {
    if (window.confirm('¿Estás seguro de eliminar a este cliente?')) {
      try {
        const response = await fetch(`http://localhost:5000/api/clients/${id}`, {
          method: 'DELETE',
        });
  
        const result = await response.json();
        if (result.success) {
          setClientes(clientes.filter(cliente => cliente._id !== id));
          console.log('Cliente eliminado');
        } else {
          console.error('Error al eliminar cliente:', result.message);
        }
      } catch (error) {
        console.error('Error al eliminar cliente:', error);
      }
    }
  };
  

  return (
    <div className="clientes-container">
      <h1>Clientes</h1>
      <p>Total de clientes: {totalClientes}</p>
      <div>
        <Link to="/clientes/agregar">
          <button className="btn agregar-btn">Agregar Cliente</button>
        </Link>
      </div>
      {loading ? (
        <p>Cargando clientes...</p>
      ) : (
        <table className="clientes-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nombre</th>
              <th>Email</th>
              <th>Teléfono</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {clientes.map(cliente => (
              <tr key={cliente._id}>
                <td>{cliente._id}</td>
                <td>{cliente.fullName}</td>
                <td>{cliente.email}</td>
                <td>{cliente.phone}</td>
                <td>
                  <button className="btn eliminar-btn" onClick={() => eliminarCliente(cliente._id)}>
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

export default Clientes;
