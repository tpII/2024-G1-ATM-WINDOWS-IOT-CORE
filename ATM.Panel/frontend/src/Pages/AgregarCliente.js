import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './AgregarCliente.css';

function AgregarCliente() {
  const [nombre, setNombre] = useState('');
  const [email, setEmail] = useState('');
  const [phone, setPhone] = useState('');
  const navigate = useNavigate(); // Cambio aquí

  const handleSubmit = async (e) => {
    e.preventDefault();
    // Aquí iría la lógica para enviar los datos al servidor (API)
    console.log('Cliente agregado:', { nombre, email });
    navigate('/clientes'); // Cambio aquí: usa navigate para redirigir
  };

  return (
    <div className="agregar-cliente-container">
      <h1>Agregar Cliente</h1>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Nombre"
          value={nombre}
          onChange={(e) => setNombre(e.target.value)}
          required
        />
        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <input
          type="tel"
          placeholder="Teléfono"
          value={phone}
          onChange={(e) => setPhone(e.target.value)}
          required
        />
        <button type="submit" className="btn agregar-btn">Agregar</button>
      </form>
    </div>
  );
}

export default AgregarCliente;
