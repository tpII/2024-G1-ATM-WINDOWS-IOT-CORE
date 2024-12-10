import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import './Clientes.css'; // Asegúrate de tener un archivo de estilos

function Clientes() {
  const [clientes, setClientes] = useState([]);
  const [loading, setLoading] = useState(true);

  // Simulando la carga de datos de clientes
  useEffect(() => {
    fetchClientes();
  }, []);

  const fetchClientes = async () => {
    // Aquí iría la lógica para obtener los datos de la API
    // Simulando clientes por ahora:
    setClientes([
      { id: 1, nombre: 'Juan Pérez', email: 'juan@mail.com' },
      { id: 2, nombre: 'Ana Gómez', email: 'ana@mail.com' },
      { id: 3, nombre: 'Carlos Ruiz', email: 'carlos@mail.com' },
    ]);
    setLoading(false);
  };

  const eliminarCliente = (id) => {
    if (window.confirm('¿Estás seguro de eliminar a este cliente?')) {
      setClientes(clientes.filter(cliente => cliente.id !== id));
      // Aquí iría la lógica para eliminar el cliente de la base de datos (API)
    }
  };

  return (
    <div className="clientes-container">
      <h1>Clientes</h1>
      <div>
        <Link to="/clientes/agregar">
          <button className="btn agregar-btn">Agregar Cliente</button>
        </Link>
        <button className="btn eliminar-btn" onClick={() => eliminarCliente(1)}>
          Eliminar Cliente (Ejemplo)
        </button>
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
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {clientes.map(cliente => (
              <tr key={cliente.id}>
                <td>{cliente.id}</td>
                <td>{cliente.nombre}</td>
                <td>{cliente.email}</td>
                <td>
                  <button className="btn eliminar-btn" onClick={() => eliminarCliente(cliente.id)}>
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
