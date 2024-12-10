import React from 'react';
import { Link } from 'react-router-dom';
import './Home.css'; // Puedes agregar estilos personalizados aquí si lo deseas.

function Home() {
  return (
    <div className="App-header">
      <h1>Bienvenido al Tablero Web de Administrador</h1>
      <p>Seleccione una opción para gestionar el sistema:</p>
      <div className="button-container">
        <Link to="/cuentas" className="home-button">Cuentas</Link>
        <Link to="/clientes" className="home-button">Clientes</Link>
        <Link to="/tarjetas" className="home-button">Tarjetas</Link>
        <Link to="/transacciones" className="home-button">Transacciones</Link>
      </div>
    </div>
  );
}

export default Home;
