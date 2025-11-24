import { useEffect, useState } from "react";
import "./Tabla.css";

const API = "http://localhost:8001/api";

export default function Horarios() {
  const [horarios, setHorarios] = useState([]);

  useEffect(() => {
    async function cargar() {
      const mascotas = await fetch(`${API}/mascotas`).then(res => res.json());
      const all = [];

      for (let m of mascotas) {
        const h = await fetch(`${API}/horarios/mascota/${m.id}`).then(res =>
          res.json()
        );

        h.forEach(x =>
          all.push({
            ...x,
            mascota: m.nombre
          })
        );
      }
      setHorarios(all);
    }

    cargar();
  }, []);

  async function eliminar(id) {
    await fetch(`${API}/horarios/${id}`, { method: "DELETE" });
    setHorarios(prev => prev.filter(x => x.id !== id));
  }

  return (
    <div className="tabla-wrapper">
      <h1 className="tabla-title">🕒 Horarios de Alimentación</h1>

      <table className="tabla">
        <thead>
          <tr>
            <th>Mascota</th>
            <th>Hora</th>
            <th>Cantidad</th>
            <th>Opciones</th>
          </tr>
        </thead>

        <tbody>
          {horarios.map(h => (
            <tr key={h.id}>
              <td>{h.mascota}</td>
              <td>{h.hora}</td>
              <td>{h.cantidadGramos} g</td>
              <td>
                <button className="btn-eliminar" onClick={() => eliminar(h.id)}>
                  Eliminar
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
