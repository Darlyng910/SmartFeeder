import { useEffect, useState } from "react";
import "./Tabla.css";

const API = "http://localhost:8001/api";

export default function Historial() {
  const [lista, setLista] = useState([]);

  useEffect(() => {
    async function cargar() {
      const mascotas = await fetch(`${API}/mascotas`).then(r => r.json());
      const eventos = [];

      for (let m of mascotas) {
        const e = await fetch(`${API}/alimentacion/historial/${m.id}`).then(r =>
          r.json()
        );
        e.forEach(x =>
          eventos.push({
            ...x,
            mascota: m.nombre
          })
        );
      }

      setLista(eventos);
    }

    cargar();
  }, []);

  return (
    <div className="tabla-wrapper">
      <h1 className="tabla-title">🍽️ Historial de Alimentación</h1>

      <table className="tabla">
        <thead>
          <tr>
            <th>Mascota</th>
            <th>Cantidad</th>
            <th>Fecha</th>
          </tr>
        </thead>

        <tbody>
          {lista.map(e => (
            <tr key={e.id}>
              <td>{e.mascota}</td>
              <td>{e.cantidadGramos} g</td>
              <td>{new Date(e.fechaHora).toLocaleString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
