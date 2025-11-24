import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import "./ProgramarHorario.css";

const API = "http://localhost:8001/api";

export default function ProgramarHorario() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [mascota, setMascota] = useState(null);
  const [hora, setHora] = useState("");
  const [cantidad, setCantidad] = useState("");

  useEffect(() => {
    fetch(`${API}/mascotas`)
      .then(res => res.json())
      .then(data => {
        const m = data.find(x => x.id === Number(id));
        setMascota(m);
      });
  }, [id]);

  async function guardarHorario(e) {
    e.preventDefault();

    await fetch(`${API}/horarios`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        mascotaId: Number(id),
        hora,
        cantidadGramos: Number(cantidad),
      }),
    });

    navigate("/");
  }

  if (!mascota) return <p>Cargando...</p>;

  return (
    <div className="horario-wrapper">
      <div className="horario-box">
        <h2>
          Programar Horario <span>({mascota.nombre})</span>
        </h2>

        <form onSubmit={guardarHorario} className="horario-form">
          <div className="form-group">
            <label>Hora</label>
            <input 
              type="time" 
              value={hora} 
              onChange={(e) => setHora(e.target.value)} 
              required 
            />
          </div>

          <div className="form-group">
            <label>Cantidad (gramos)</label>
            <input 
              type="number" 
              placeholder="Ej. 50" 
              value={cantidad}
              onChange={(e) => setCantidad(e.target.value)}
              required 
            />
          </div>

          <button className="btn-save">Guardar horario</button>
        </form>
      </div>
    </div>
  );
}
