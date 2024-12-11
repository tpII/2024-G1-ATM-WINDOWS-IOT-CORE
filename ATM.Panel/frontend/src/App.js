import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './Pages/Home';
import Cuentas from './Pages/Cuentas';
import Clientes from './Pages/Clientes';
import Tarjetas from './Pages/Tarjetas';
import Transacciones from './Pages/Transacciones';
import AgregarCuenta from './Pages/AgregarCuenta';
import AgregarCliente from './Pages/AgregarCliente'; 
import AgregarTarjeta from './Pages/AgregarTarjeta';
import LimitesExtraccion from './Pages/LimitesExtraccion';
import DineroDisponible from './Pages/DineroDisponible';

import './App.css';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/cuentas" element={<Cuentas />} />
        <Route path="/clientes" element={<Clientes />} />
        <Route path="/tarjetas" element={<Tarjetas />} />
        <Route path="/transacciones" element={<Transacciones />} />
        <Route path="/clientes/agregar" element={<AgregarCliente />} />
        <Route path="/cuentas/agregar" element={<AgregarCuenta />} />
        <Route path="/tarjetas/agregar" element={<AgregarTarjeta />} />
        <Route path="/limites-extraccion" element={<LimitesExtraccion />} />
        <Route path="/dinero-disponible" element={<DineroDisponible />} />
      </Routes>
    </Router>
  );
}

export default App;
