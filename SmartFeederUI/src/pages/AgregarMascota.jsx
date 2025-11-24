import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./AgregarMascota.css";

const API = "http://localhost:8001/api";

export default function AgregarMascota() {
  const [nombre, setNombre] = useState("");
  const [especie, setEspecie] = useState("");
  const [edad, setEdad] = useState("");
  const navigate = useNavigate();

  async function guardarMascota(e) {
    e.preventDefault();

    await fetch(`${API}/mascotas`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        nombre,
        especie,
        edad: Number(edad),
      }),
    });

    navigate("/");
  }

  return (
    <div className="form-wrapper">
      <div className="form-box">
        <h2 className="form-title">Agregar Mascota</h2>

        <form onSubmit={guardarMascota} className="form">
          <div className="form-group">
            <label>Nombre</label>
            <input
              type="text"
              placeholder="Luna"
              value={nombre}
              onChange={(e) => setNombre(e.target.value)}
              required
            />
          </div>

          <div className="form-group">
            <label>Especie</label>
            <input
              type="text"
              placeholder="Gato"
              value={especie}
              onChange={(e) => setEspecie(e.target.value)}
              required
            />
          </div>

          <div className="form-group">
            <label>Edad (años)</label>
            <input
              type="number"
              placeholder="3"
              value={edad}
              onChange={(e) => setEdad(e.target.value)}
              required
            />
          </div>

          <button className="btn-save">Guardar</button>
        </form>
      </div>
    </div>
  );
}