import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import './Cuentas.css'; 

function Cuentas() {
  const [cuentas, setCuentas] = useState([]);
  const [loading, setLoading] = useState(true);
  const [totalCuentas, setTotalCuentas] = useState(0);

  // Simulando la carga de datos de cuentas
  useEffect(() => {
    fetchCuentas();
  }, []);

  const fetchCuentas = async () => {
    try {
      const response = await fetch('http://localhost:5000/api/accounts');
      const data = await response.json();
      setCuentas(data.data);  // Asumiendo que 'data' es la clave que contiene la lista de clientes
      setTotalCuentas(data.data.length); 
      setLoading(false);
    } catch (error) {
      console.log('Error al cargar las cuentas:', error);
      setLoading(false);
    }
  };

  const eliminarCuenta = async (id) => {
    if (window.confirm('¿Estás seguro de eliminar esta cuenta?')) {
      try {
        const response = await fetch(`http://localhost:5000/api/accounts/${id}`, {
          method: 'DELETE',
        });
  
        const result = await response.json();
        if (result.success) {
          setCuentas(cuentas.filter(cuenta => cuenta._id !== id));
          console.log('Cuenta eliminada');
        } else {
          console.error('Error al eliminar cuenta:', result.message);
        }
      } catch (error) {
        console.error('Error al eliminar cuenta:', error);
      }
    }
  };

  return (
    <div className="cuentas-container">
      <h1>Cuentas</h1>
      <p>Total de cuentas: {totalCuentas}</p>
      <div>
        <Link to="/cuentas/agregar">
          <button className="btn agregar-btn">Agregar Cuenta</button>
        </Link>
      </div>
      {loading ? (
        <p>Cargando cuentas...</p>
      ) : (
        <table className="cuentas-table">
          <thead>
            <tr>
              <th>Número de Cuenta</th>
              <th>CBU</th>
              <th>Balance</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {cuentas.map(cuenta => (
              <tr key={cuenta.id}>
                <td>{cuenta.number}</td>
                <td>{cuenta.cbu}</td>
                <td>${cuenta.balance}</td>
                <td>
                  <button className="btn eliminar-btn" onClick={() => eliminarCuenta(cuenta._id)}>
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

export default Cuentas;
