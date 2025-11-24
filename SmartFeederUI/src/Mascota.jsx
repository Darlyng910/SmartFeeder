import { useEffect, useState } from "react";

const API = "http://localhost:8001/api";

export default function Mascotas() {
  const [mascotas, setMascotas] = useState([]);
  const [nombre, setNombre] = useState("");
  const [especie, setEspecie] = useState("");
  const [edad, setEdad] = useState("");

  async function cargarMascotas() {
    const res = await fetch(`${API}/mascotas`);
    const data = await res.json();
    setMascotas(data);
  }

  async function crearMascota() {
    await fetch(`${API}/mascotas`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        nombre,
        especie,
        edad: Number(edad),
      }),
    });

    setNombre("");
    setEspecie("");
    setEdad("");
    cargarMascotas();
  }

  useEffect(() => {
  async function fetchData() {
    await cargarMascotas();
  }
  fetchData();
}, []);

  return (
    <div style={{ padding: "20px" }}>
      <h1>🐶 Mascotas</h1>

      <h2>Agregar Mascota</h2>
      <input
        placeholder="Nombre"
        value={nombre}
        onChange={(e) => setNombre(e.target.value)}
      />
      <input
        placeholder="Especie"
        value={especie}
        onChange={(e) => setEspecie(e.target.value)}
      />
      <input
        placeholder="Edad"
        value={edad}
        onChange={(e) => setEdad(e.target.value)}
        type="number"
      />
      <button onClick={crearMascota}>Guardar</button>

      <h2>Lista de Mascotas</h2>
      <ul>
        {mascotas.map((m) => (
          <li key={m.id}>
            {m.nombre} - {m.especie} - {m.edad} años
          </li>
        ))}
      </ul>
    </div>
  );
}