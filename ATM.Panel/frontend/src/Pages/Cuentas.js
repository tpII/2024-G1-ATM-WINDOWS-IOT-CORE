import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import './Cuentas.css'; // Asegúrate de tener un archivo de estilos

function Cuentas() {
  const [cuentas, setCuentas] = useState([]);
  const [loading, setLoading] = useState(true);
  const [totalCuentas, setTotalCuentas] = useState(0);

  // Simulando la carga de datos de cuentas
  useEffect(() => {
    fetchCuentas();
  }, []);

  const fetchCuentas = async () => {
    // Aquí iría la lógica para obtener las cuentas desde el backend
    // Simulando cuentas por ahora:
    const cuentasData = [
      { id: 1, number: '123456789', cbu: '1234567890', balance: 5000 },
      { id: 2, number: '987654321', cbu: '0987654321', balance: 10000 },
      { id: 3, number: '112233445', cbu: '1122334455', balance: 15000 },
    ];
    setCuentas(cuentasData);
    setTotalCuentas(cuentasData.length); // Total de cuentas
    setLoading(false);
  };

  const eliminarCuenta = (id) => {
    if (window.confirm('¿Estás seguro de eliminar esta cuenta?')) {
      setCuentas(cuentas.filter(cuenta => cuenta.id !== id));
      // Aquí iría la lógica para eliminar la cuenta desde la API
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
                  <button className="btn eliminar-btn" onClick={() => eliminarCuenta(cuenta.id)}>
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
