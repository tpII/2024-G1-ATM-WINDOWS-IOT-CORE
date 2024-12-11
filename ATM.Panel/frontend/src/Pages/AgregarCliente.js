import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './AgregarCliente.css';

function AgregarCliente() {
  const [nombre, setNombre] = useState('');
  const [email, setEmail] = useState('');
  const [phone, setPhone] = useState('');
  const navigate = useNavigate(); 

  const handleSubmit = async (e) => {
    e.preventDefault();
  
    const cliente = { fullName: nombre, email: email, phone: phone };
  
    try {
      const response = await fetch('http://localhost:5000/api/clients', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(cliente),
      });
  
      const result = await response.json();
      if (result.success) {
        console.log('Cliente agregado:', result.data);
        navigate('/clientes'); // Redirige después de agregar
      } else {
        console.log('Error al agregar cliente:', result.message);
      }
    } catch (error) {
      console.error('Error en el servidor:', error);
    }
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
