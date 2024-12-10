import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './Pages/Home';
import Cuentas from './Pages/Cuentas';
import Clientes from './Pages/Clientes';
import Tarjetas from './Pages/Tarjetas';
import Transacciones from './Pages/Transacciones';
import AgregarCliente from './Pages/AgregarCliente'; // o la ubicación correcta
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
      </Routes>
    </Router>
  );
}

export default App;
